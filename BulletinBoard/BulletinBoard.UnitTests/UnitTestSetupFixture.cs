using BulletinBoard.Models;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BulletinBoard.UnitTests
{
    [SetUpFixture]
    class UnitTestSetupFixture
    {
        [SetUp]
        public void Setup()
        {
            var kernel = new StandardKernel();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            RegisterServices(kernel);
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRepository>().To<MockRepository>().InTransientScope();
        }      

        [TearDown]
        public void TearDown()
        {
        }
    }
}
