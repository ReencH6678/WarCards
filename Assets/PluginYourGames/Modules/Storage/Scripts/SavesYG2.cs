
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave = 1;
        public List<CardSaveData> InventoryCards = new List<CardSaveData>();    
        public List<CardSaveData> DeckCards = new List<CardSaveData>();    
    }
}
