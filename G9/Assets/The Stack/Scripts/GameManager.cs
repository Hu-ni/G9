using G9.Const;
using G9.Game.LeaderBoard;
using G9.Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace G9.MiniGame.TheStack
{
    public class GameManager : MonoBehaviour
    {
        // UI: 스코어, 콤보
        // 시스템: 게임 오버, 색 바꿈, 외곽선
        // 인게임: 블럭 생성,

        // Const Value
        private const float BoundSize = 3.5f;
        private const float StackMovingSpeed = 5.0f;    //Move에서만 사용

        [SerializeField]
        private GameObject _originBlock = null;

        private Vector3 prevBlockPosition;
        private Vector3 desiredPosition;
        private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

        private Transform _lastBlock = null; // 현재 들고 있는 블럭(Move, Place에 사용)
        //float blockTransition = 0f; // Move로 이동
        float secondaryPosition = 0f;

        private int _stackCount = -1;

        public int Score { get { return _stackCount; } }

        //public Color prevColor;
        //public Color nextColor;

        // 블록 색 변경
        private BlockColorizer _blockColorizer;
        private ScoreManager _scoreManager;
        private BlockMover _blockMover;
        private BlockPlacer _blockPlacer;
        
        private ComboTracker _comboTracker;
        private RubbleSpawner _rubbleSpawner;

        public int Combo => _comboTracker.Combo;
        public int MaxCombo => _comboTracker.MaxCombo;
        public int BestScore => _scoreManager.BestScore;
        public int BestCombo => _scoreManager.BestCombo;

        bool isMovingX = true;
        bool isGameOver = true;


        void Start()
        {

            if (_originBlock == null)
            {
                Debug.Log("OriginBlock is NULL");
                return;
            }



            InitializeModules();

            //prevColor = GetRandomColor();
            //nextColor = GetRandomColor();
            _blockColorizer.Reset();
            prevBlockPosition = Vector3.down;
            //Spawn_Block();
            SpawnBlock();
        }

        void Update()
        {
            if (isGameOver)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (_blockPlacer.PlaceBlock(_lastBlock, isMovingX, prevBlockPosition, ref stackBounds, ref secondaryPosition))
                {
                    SpawnBlock();
                }
                else
                {
                    // 게임 오버
                    Debug.Log("GameOver");
                    _scoreManager.UpdateScore(_stackCount, _comboTracker.MaxCombo);
                    isGameOver = true;
                    GameOverEffect();
                    UIManager.Instance.SetScoreUI();
                }
            }

            _blockMover.MoveBlock(_lastBlock,_stackCount, isMovingX, secondaryPosition);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        }

        private void InitializeModules()
        {
            _scoreManager = new ScoreManager();
            _comboTracker = new ComboTracker();
            _rubbleSpawner = new RubbleSpawner();
            _blockColorizer = new BlockColorizer();
            _blockMover = new BlockMover();
            _blockPlacer = new BlockPlacer(_rubbleSpawner, _comboTracker);
        }

        // 블럭 생성
        void SpawnBlock()
        {
            if(_lastBlock != null)
                prevBlockPosition = _lastBlock.localPosition;

            GameObject newBlock = Instantiate(_originBlock);

            if (newBlock == null)
            {
                Debug.Log("NewBlock Instantiate Failed!");
                return;
            }

            Transform newTrans = newBlock.transform;
            newTrans.parent = this.transform;
            newTrans.localPosition = prevBlockPosition + Vector3.up;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y); //현재 블럭의 크기

            _stackCount++;
            
            desiredPosition = Vector3.down * _stackCount;    // 스택이 쌓였으니 위치 내리기
            _blockMover.Reset();

            _lastBlock = newTrans;
            _blockColorizer.ApplyColor(newBlock, _stackCount);
            isMovingX = !isMovingX;

            UIManager.Instance.UpdateScore();
        }

        void GameOverEffect()
        {
            int childCount = this.transform.childCount;

            for (int i = 1; i < 20; i++)
            {
                if (childCount < i)
                    break;

                GameObject go =
                    this.transform.GetChild(childCount - i).gameObject;

                if (go.name.Equals("Rubble"))
                    continue;

                Rigidbody rigid = go.AddComponent<Rigidbody>();

                rigid.AddForce(
                    (Vector3.up * Random.Range(0, 10f)
                     + Vector3.right * (Random.Range(0, 10f) - 5f))
                    * 100f
                );
            }
        }

        public void Restart()
        {
            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            isGameOver = false;

            _lastBlock = null;
            desiredPosition = Vector3.zero;
            stackBounds = new Vector3(BoundSize, BoundSize);

            _stackCount = -1;
            isMovingX = true;
            _blockMover.Reset();
            //blockTransition = 0f;
            secondaryPosition = 0f;

            _comboTracker.Initialize();

            prevBlockPosition = Vector3.down;

            _blockColorizer.Reset();
            //prevColor = GetRandomColor();
            //nextColor = GetRandomColor();

            SpawnBlock();
            SpawnBlock();
        }

        public void SaveLeaderBoardData()
        {
            var entry = new LeaderBoardEntry
            {
                playerName = "Test",
                score = Score,
                //playTime = currentPlayTime,
                timestamp = System.DateTime.Now.ToString("s"),
                extraData = new Dictionary<string, string>
                {
                    { "maxCombo", MaxCombo.ToString() }
                }
            };

            // 기존 랭킹 읽기
            var leaderboard = LeaderboardFileUtil.LoadLeaderboard(ConstValues.TheStack);

            // 기록 추가 + 정렬
            leaderboard.entries.Add(entry);
            leaderboard.entries = leaderboard.entries
                .OrderByDescending(e => e.score)
                .Take(100)
                .ToList();

            // 파일로 저장
            LeaderboardFileUtil.SaveLeaderBoard(ConstValues.TheStack, leaderboard);
        }
    }
}
