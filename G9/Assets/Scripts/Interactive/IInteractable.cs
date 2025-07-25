using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G9.Game.Interactive
{
    // 상호작용 인터페이스
    public interface IInteractable
    {
        void Interact();
        string GetPromptMessage();
    }
}
