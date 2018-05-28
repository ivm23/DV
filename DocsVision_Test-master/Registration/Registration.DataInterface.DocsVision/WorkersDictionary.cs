using System;
using System.Collections.Generic;

using DocsVision.Platform.ObjectManager;

namespace Registration.DataInterface.DocsVision
{
    public class WorkersDictionary
    {
        private readonly CardData _cardDictionary;
        private IEnumerable<Worker> _allWorkers = new List<Worker>();

        private readonly Guid WorkersSectionId = new Guid("4274DBA2-D96E-4857-B54C-F6D4C5F7C5E3");
        private readonly SectionData _sectionData;

        public WorkersDictionary(CardData cardData)
        {
            _cardDictionary = cardData;
            _sectionData = CardDictionary.Sections[WorkersSectionId];
        }

        private SectionData SectionData
        {
            get { return _sectionData; }
        }
        private CardData CardDictionary
        {
            get { return _cardDictionary; }
        }

        public RowData AddNew()
        {
            return SectionData.CreateRow();
        }

    }
}
