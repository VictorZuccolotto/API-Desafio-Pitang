using DesafioPitang.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace DesafioPitang.UnitTests
{
    public class UnitTestBase
    {
        private readonly IServiceCollection ServiceCollection = new ServiceCollection();

        protected Mock<T> RegistrarMock<T>() where T : class
        {
            var mock = new Mock<T>();

            ServiceCollection.AddSingleton(typeof(T), mock.Object);

            return mock;
        }

        protected void Registrar<I, T>() where I : class where T : class, I
          => ServiceCollection.AddSingleton<I, T>();

        protected I ObterServico<I>() where I : class
          => ServiceCollection.BuildServiceProvider().GetService<I>();

        protected void RegistrarObjeto<Tp, T>(Tp type, T objeto) where Tp : Type where T : class
           => ServiceCollection.AddSingleton(type, objeto);
    }
}