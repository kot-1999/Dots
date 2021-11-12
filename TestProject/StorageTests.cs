using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dots.comments;

namespace TestProject
{
    [TestClass]
    public class StorageTests
    {
     

        [TestMethod]
        public void StorageAddTest()
        {
            Storage s = new SerializationStorage();
            s.Add("alex","one");
            s.Add("peter","two");
            s.Add("alex","three");
            s.Add("peter","four");

            List<KeyValuePair<string, string>> list= s.Get();
            Assert.AreEqual("alex",list[0].Key);
            Assert.AreEqual("peter",list[1].Key);
            Assert.AreEqual("alex",list[2].Key);
            Assert.AreEqual("peter",list[3].Key);
            
            Assert.AreEqual("one",list[0].Value);
            Assert.AreEqual("two",list[1].Value);
            Assert.AreEqual("three",list[2].Value);
            Assert.AreEqual("four",list[3].Value);
            
        }
    
        [TestMethod]
        public void StorageGetValueTest()
        {
            Storage s = new SerializationStorage();
            s.Add("alex","d");
            s.Add("peter","c");
            s.Add("alex","b");
            s.Add("peter","a");

        
            Assert.AreEqual("d",s.GetValue("alex"));
            Assert.AreEqual("c",s.GetValue("peter"));
       
            
        }
        [TestMethod]
        public void StorageSortListWithWrongTypeTest()
        {
            Storage s = new SerializationStorage();
            s.Add("alex","d");
            s.Add("peter","c");
            s.Add("alex","b");
            s.Add("peter","a");

            s.SortList();
            Assert.AreEqual("d",s.GetValue("alex"));
            Assert.AreEqual("c",s.GetValue("peter"));
       
            
        }
        
        [TestMethod]
        public void StorageSortListWithCorrectTypeTest()
        {
            Storage s = new SerializationStorage();
            s.Add("alex","10");
            s.Add("peter","15");
            s.Add("alex","100");
            s.Add("peter","100");

            s.SortList();
            Assert.AreEqual("100",s.GetValue("alex"));
            Assert.AreEqual("100",s.GetValue("peter"));
        }

        [TestMethod]
        public void StorageSaveAndLoadFileTest()
        {
            string filename = "test.bin";
            Storage s1 = new SerializationStorage();
            s1.SetFile(filename);
            s1.Refresh();
            s1.Add("alex","one");
            s1.Add("peter","two");
            s1.save();
            
            Storage s2 = new SerializationStorage();
            s2.SetFile(filename);
            s2.load();
            
            List<KeyValuePair<string, string>> list= s2.Get();
            Assert.AreEqual("alex",list[0].Key);
            Assert.AreEqual("peter",list[1].Key);

            Assert.AreEqual("one",list[0].Value);
            Assert.AreEqual("two",list[1].Value);
        }
    }
}