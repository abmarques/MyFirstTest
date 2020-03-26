using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{

    [TestClass]
    public class FileProcessTest
    {

        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private string _GoodFileName;

        public TestContext TestContext { get; set; }

        #region Tests Initialize & Cleanup

        [TestInitialize]
        public void TestInitialize()
        {

            if (TestContext.TestName == "FileNameDoesExists")
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Creating file: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {

            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {

                    TestContext.WriteLine($"Deleting file: {_GoodFileName}");
                    File.Delete(_GoodFileName);

                }
            }
        }

        #endregion

        [TestMethod]
        public void FileNameDoesExists()
        {

            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine($"Testing file: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);


            Assert.IsTrue(fromCall);

        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            }
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(@"C:\Regedit.exe");

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            //TODO
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            //TODO
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                //Test was succesful
                return;
            }

            Assert.Fail("Fail expected");

        }
    }
}
