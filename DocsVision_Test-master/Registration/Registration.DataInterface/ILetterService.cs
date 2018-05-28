using System;
using System.Collections.Generic;

namespace Registration.DataInterface
{
    public interface ILetterService
    {
        ILetter Create(ILetterView letter);
        ILetterView Get(Guid folderId, Guid workerId);
        void Delete(Guid letterId, Guid workerId, Guid folderId);
        void ChangeLetterIsRead(Guid letterId, Guid workerId);

        IEnumerable<ILetterType> GetAllLetterTypes();
        ILetterType GetLetterType(int letterTypeId);
    }
}
