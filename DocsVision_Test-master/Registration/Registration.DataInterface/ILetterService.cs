using System;
using System.Collections.Generic;
using Registration.Model;

namespace Registration.DataInterface
{
    public interface ILetterService
    {
        Letter Create(LetterView letter);
        LetterView Get(Guid folderId, Guid workerId);
        void Delete(Guid letterId, Guid workerId, Guid folderId);
        void ChangeLetterIsRead(Guid letterId, Guid workerId);

        IEnumerable<LetterType> GetAllLetterTypes();
        LetterType GetLetterType(int letterTypeId);
    }
}
