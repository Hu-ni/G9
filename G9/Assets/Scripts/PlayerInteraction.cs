using G9.Game.Interactive;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace G9.Game.Interactive
{
    public class PlayerInteraction : MonoBehaviour
    {
        // 플레이어 상호작용 감시자
        // 주변에 상호작용 가능한 오브젝트 있는지 검색.
        public Material outlineMaterial;
        private Renderer lastTargetRenderer;
        private Material originalMaterial;


        public float detectRadius = 1.5f;
        public LayerMask interactableLayer;
        private IInteractable currentTarget;

        void Update()
        {
            DetectNearbyInteractable();

            if (currentTarget != null && Input.GetKeyDown(KeyCode.F))
                currentTarget.Interact();
        }
        void DetectNearbyInteractable()
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, detectRadius, interactableLayer);
            if (col && col.TryGetComponent(out IInteractable interactable))
            {
                // 새로운 대상을 찾았고, 이전 대상과 다르면 이전 것을 되돌림
                if (lastTargetRenderer != null && lastTargetRenderer.gameObject != col.gameObject)
                {
                    lastTargetRenderer.material = originalMaterial;
                }

                currentTarget = interactable;

                //외곽선 처리
                Renderer rend = col.GetComponent<Renderer>();
                if (rend != null)
                {
                    // 처음 감지된 경우에만 원본 머티리얼 저장
                    if (rend != lastTargetRenderer)
                    {
                        originalMaterial = rend.material;
                    }

                    rend.material = outlineMaterial;
                    lastTargetRenderer = rend;
                }
                Debug.Log("상호작용 구조물 근처에 있음");
            }
            else
            {
                // 감지된 오브젝트가 없으면 이전 대상 복구
                if (lastTargetRenderer != null)
                {
                    lastTargetRenderer.material = originalMaterial;
                    lastTargetRenderer = null;
                    originalMaterial = null;
                }

                currentTarget = null;
            }
        }
    }
}

