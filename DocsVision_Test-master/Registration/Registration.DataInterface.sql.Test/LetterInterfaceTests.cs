using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration.Model;
using Registration.DataInterface;
using Registration.DataInterface.Sql;

namespace Registration.DataInterface.sql.Test
{
    [TestClass]
    public class LetterInterfaceTests
    {
        private const string ConnectionString = @"Data Source = (local)\SQLEXPRESS;
                                                    Initial Catalog = Registration.DB;
                                                    Integrated Security = True";

        [TestMethod]
        public void ShouldCreateLetter()
        {
            List<Guid> receivers = new List<Guid> { new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"), new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4") };

            string nameLetter = "letter1";
            Guid sender = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

            string msg = "flkgjsflkgfd";

            var letterInterface = new LetterService(ConnectionString);
            var letter = letterInterface.Create(nameLetter, receivers, sender, msg);

            Assert.AreEqual(letter.idSender, sender);
            Assert.AreEqual(letter.text, msg);
            Assert.AreEqual(letter.name, nameLetter);

            Assert.AreEqual(letter.idReceivers.Count, receivers.Count);

            for (int i = 0; i < receivers.Count; ++i)
            {
                Assert.AreEqual(letter.idReceivers[i], receivers[i]);
            }

        }
        [TestMethod]
        public void ShouldGetLetter()
        {
            List<Guid> receivers = new List<Guid> { new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"), new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4") };

            string nameLetter = "letter1";
            Guid sender = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

            string msg = "flkgjsflkgfd";

            var letterInterface = new LetterService(ConnectionString);
            var letter = letterInterface.Create(nameLetter, receivers, sender, msg);

            var getLetter = letterInterface.Get(letter.id);

            Assert.AreEqual(letter.id, getLetter.id);
            Assert.AreEqual(letter.idSender, getLetter.idSender);
            Assert.AreEqual(letter.text, getLetter.text);
            Assert.AreEqual(letter.name, getLetter.name);

            Assert.AreEqual(letter.idReceivers.Count, getLetter.idReceivers.Count);

            for (int i = 0; i < receivers.Count; ++i)
            {
                Assert.AreEqual(letter.idReceivers[i], getLetter.idReceivers[i]);
            }

        }
        [TestMethod]
        public void ShouldGetIdReceivers()
        {
            List<Guid> receivers = new List<Guid> { new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"), new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4") };

            string nameLetter = "letter1";
            Guid sender = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

            string msg = "flkgjsflkgfd";

            var letterInterface = new LetterService(ConnectionString);
            var letter = letterInterface.Create(nameLetter, receivers, sender, msg);

            var idRec = letterInterface.GetIdReceivers(letter.id);
            Assert.AreEqual(idRec.Count, receivers.Count);
            for (int i = 0; i < receivers.Count; ++i)
            {
                Assert.AreEqual(idRec[i], receivers[i]);
            }
        }

        [TestMethod]
        public void ShouldGetReceivers()
        {
            List<Guid> receivers = new List<Guid> { new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"), new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4") };

            string nameLetter = "letter1";
            Guid sender = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

            string msg = "flkgjsflkgfd";

            var letterInterface = new LetterService(ConnectionString);
            var letter = letterInterface.Create(nameLetter, receivers, sender, msg);
            var rec = letterInterface.GetReceivers(letter.id);

            Assert.AreEqual(rec.Count, receivers.Count);          
        }
    }
}
