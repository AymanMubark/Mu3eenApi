using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Mu3een.Data;
using Mu3een.Entities;
using Mu3een.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Mu3een.IServices;

namespace Mu3een.Test
{

    public class RegionServiceTests
    {
        private DbContextOptions<Mu3eenContext> dbContextOptions = new DbContextOptionsBuilder<Mu3eenContext>().UseInMemoryDatabase(databaseName: "Mu3eenDB").Options;

        private IRegionService regionService;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDb();

            regionService = new RegionService(new Mu3eenContext(dbContextOptions));
        }

        [Test]
        public async Task GetAllTest()
        {
            using var context = new Mu3eenContext(dbContextOptions);

            var regions = (await regionService.GetAll()).ToList();

            regions.Count.Should().Be(1);
            regions.All(r => r.Name == null).Should().BeFalse();
        }

        [Test]
        public async Task GetByIdTestWhenIdNotExist()
        {
            using var context = new Mu3eenContext(dbContextOptions);

            await regionService.Invoking(async y => await y.GetById(Guid.NewGuid()))
                .Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Region not found");
        }

        [Test]
        public async Task GetByIdTestWhenIdExist()
        {
            //arrange
            using var context = new Mu3eenContext(dbContextOptions);
            //Action
            var region = await regionService.GetById(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
            //Assert
            region.Should().NotBeNull();

        }

        private void SeedDb()
        {
            using var context = new Mu3eenContext(dbContextOptions);
            var regions = new List<Region>
        {
            new Region { Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "Righd", NameAr = "الرياض" },
        };
            context.AddRange(regions);
            context.SaveChanges();
        }
    }
}