using System;
using System.Collections.Generic;
using Registration.Model;

namespace Registration.DataInterface
{
    public interface ILetterService
    {
        ILetter Create(LetterView letter);
        ILetterView Get(Guid folderId, Guid workerId);
        void Delete(Guid letterId, Guid workerId, Guid folderId);
        void ChangeLetterIsRead(Guid letterId, Guid workerId);

        IEnumerable<ILetterType> GetAllLetterTypes();
        ILetterType GetLetterType(int letterTypeId);
    }
}
