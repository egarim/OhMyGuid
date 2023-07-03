using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OhMyGuid.Tests
{

    public class RegularGuidTest
    {

        [SetUp()]
        public void Setup()
        {

        }



        [Test]
        public void TestGuidsAreUnique()
        {
            var guids = new HashSet<Guid>();
            for (int i = 0; i < 100; i++)
            {
                Guid guid = GuidService.RegularGuid();
                Assert.False(guids.Contains(guid));
                guids.Add(guid);
            }
        }

        [Test]
        public void TestGuidsAreSequential()
        {
            Guid first = GuidService.RegularGuid();
            Thread.Sleep(1);
            Guid second = GuidService.RegularGuid();
            Thread.Sleep(1);
            Guid third = GuidService.RegularGuid();

            Assert.True(first.CompareTo(second) < 0);
            Assert.True(second.CompareTo(third) < 0);
        }
        [Test]
        public void TestGuidsAreSortableAndMaintainOrder()
        {
            var guidsWithCreationTime = new List<(Guid guid, long ticks)>();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1);
                var guid = GuidService.RegularGuid();
                guidsWithCreationTime.Add((guid, GuidService.ExtractTicksFromGuid(guid)));
            }

            var sortedGuids = guidsWithCreationTime.OrderBy(g => g.ticks).Select(g => g.guid).ToList();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(guidsWithCreationTime[i].guid, sortedGuids[i]);
            }
        }
        [Test]
        public void TestGuidsOrderComparison()
        {
            var guidsWithCreationTime = new List<(Guid guid, long ticks)>();
            for (int i = 0; i < 10; i++)
            {
                //Thread.Sleep(1);
                var guid = GuidService.RegularGuid();
                guidsWithCreationTime.Add((guid, GuidService.ExtractTicksFromGuid(guid)));
            }

            var Position2 = guidsWithCreationTime[1].guid;
            var GreaterThanPosition2 = guidsWithCreationTime.Count(item => item.guid.CompareToByTimestamp(Position2) > 0);




        }

    }
}