using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestLayer
{
    public class AreaContextUnitTest
    {
        private SocialMediaDbContext dbContext;
        private AreaContext areaContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new SocialMediaDbContext(builder.Options);
            areaContext = new AreaContext(dbContext);
        }

        [Test]
        public void TestAreaCreate()
        {
            int oldCount = areaContext.ReadAll().Count();
            areaContext.Create(new Area("area1"));
            int newCount = areaContext.ReadAll().Count();
            Assert.AreNotEqual(oldCount, newCount);
        }

        [Test]
        public void TestAreaRead()
        {
            areaContext.Create(new Area("area1"));
            Area readArea = areaContext.Read(1);
            Assert.IsNotNull(readArea);
        }

        [Test]
        public void TestAreaUpdate()
        {
            Area area = areaContext.Create(new Area("area1"));
            string oldArea = areaContext.Read(1).Name;
            area.Name = "area2";
            areaContext.Update(area);
            string newArea = areaContext.Read(1).Name;
            Assert.AreNotSame(oldArea, newArea);
        }

        [Test]
        public void TestAreaDelete()
        {
            areaContext.Create(new Area("area1"));
            int oldCount = areaContext.ReadAll().Count();
            areaContext.Delete(1);
            int newCount = areaContext.ReadAll().Count();
            Assert.AreNotEqual(oldCount, newCount);
        }
    }
}
