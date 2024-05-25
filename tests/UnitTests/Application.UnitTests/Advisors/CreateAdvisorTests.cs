﻿using Apexa.AdvisorApp.Application;
using Apexa.AdvisorApp.Application.Advisors.Commands.CreateAdvisor;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Application.Mappings;
using Apexa.AdvisorApp.Domain.Entities;
using Apexa.App.Advisor.Domain.Enumerations;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Application.UnitTests.Advisors
{
    public class CreateAdvisorTests
    {

        public CreateAdvisorTests()
        {

        }


        [Fact]
        public async Task Handle_ValidAdvisor_Added()
        {
            var configurationProvider = new MapperConfiguration(AutoMappingConfig.GetConfig([typeof(MapperProfile)]));
            var logger = new Mock<ILogger<CreateAdvisorCommandHandler>>();
            var mockAdvisorRepository = new Mock<IAdvisorRepository>();




            var advisorList = new List<Advisor>()
            {
                new Advisor()
                {
                    Id =new Guid("EFB63F5E-3096-4B81-8692-39A1031FB0EB"),
                    Name="Test Advisor",
                    SIN = "123456789",
                    Address = "TEST Address",
                    Phone = "12345678",
                    Status = HealthStatus.Yellow,

                }
            };


            mockAdvisorRepository.Setup(r => r.GetAdvisorsAsync(1, 10)).ReturnsAsync((advisorList.Count, advisorList));
            mockAdvisorRepository.Setup(repo => repo.AddAsync(It.IsAny<Advisor>())).ReturnsAsync(
               (Advisor advisor) =>
               {
                   advisorList.Add(advisor);
                   return advisor;
               });

            var handler = new CreateAdvisorCommandHandler(logger.Object, configurationProvider.CreateMapper(), mockAdvisorRepository.Object);
            await handler.Handle(new CreateAdvisorCommand()
            {
                Name = "Test Advisor2",
                SIN = "123456789",
                Address = "TEST Address",
                Phone = "12345678",
                Status = HealthStatus.Yellow,

            }, CancellationToken.None);
            var (total,allAdvisors) = await mockAdvisorRepository.Object.GetAdvisorsAsync(1,10);
            allAdvisors.Count.Should().Be(2);

        }
    }
}
