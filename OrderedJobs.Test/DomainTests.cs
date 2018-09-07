using System;
using NUnit.Framework;

namespace OrderedJobs.Test
{
    [TestFixture]
    public class DomainTests
    {
        private string _dependencies;
        private string _actual;
        private string _expected;
        
        private void AssertActualEqualsExpected()
        {
            _actual = Domain.OrderedJobs.OrderTheJobs(_dependencies).Result;
            Assert.That(_actual, Is.EqualTo(_expected));
        }

        [Test]
        public void ReturnJobsInOrder()
        {
            _dependencies = "a => | b => | c => ";
            _expected = "abc";
            AssertActualEqualsExpected();
        }

        [Test]
        public void EmptyStringShouldReturnEmptySequence()
        {
            _dependencies = "";
            _expected = "";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void StringAShouldReturnSingleJobA()
        {
            _expected = "a";
            _dependencies = "a => ";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void StringAShouldReturnSingleJobB()
        {
            _expected = "b";
            _dependencies = "b => ";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void MultipleJobsShouldReturnThoseJobs()
        {
            _expected = "abc";
            _dependencies = "a => | b => | c => ";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void MultipleJobsSingleDependancy()
        {
            _expected = "acb";
            _dependencies = "a => | b => c | c => ";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void MultipleJobsTwoDependancies()
        {
            _expected = "afcb";
            _dependencies = "a => | b => c | c => f | f => ";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void MultipleJobsMultipleDependancies()
        {
            _expected = "adfcbe";
            _dependencies = "a => | b => c | c => f | d => a | e => b | f => ";
            AssertActualEqualsExpected();
        } 
        
        [Test]
        public void ThreeJobsMultipleDependancies()
        {
            _expected = "cba";
            _dependencies = "a => b | c => | b => c ";
            AssertActualEqualsExpected();
        }
        
        [Test]
        public void CircularDependencyJobs()
        {
            _expected = "circular dependancy not allowed";
            _dependencies = "a => | b => c | c => f | d => a | e => | f => b ";
            AssertActualEqualsExpected();
        }
    }
}