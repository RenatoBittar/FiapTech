using Microsoft.EntityFrameworkCore;
using PosFiapTech1.Domain.Entities;
using PosFiapTech1.Infrastructure.Data;


namespace Fiap_Tech01.Tests
{
    public class ContatoTests
    {
        private readonly ContatosContext _context;

        public ContatoTests()
        {
            var options = new DbContextOptionsBuilder<ContatosContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ContatosContext(options);
        }
        [Theory]
        [InlineData("Teste1", "teste1@example.com", "123456789", "11", "Rua Teste 1")]
        [InlineData("Teste2", "teste2@example.com", "987654321", "21", "Rua Teste 2")]
        public void TestAddContato(string nome, string email, string telefone, string ddd, string endereco)
        {
            var contato = new Contato
            {
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Ddd = ddd,
                Endereco = endereco
            };

            _context.Contatos.Add(contato);
            _context.SaveChanges();

            var result = _context.Contatos.Find(contato.Id);
            Assert.NotNull(result);
            Assert.Equal(nome, result.Nome);
            Assert.Equal(email, result.Email);
            Assert.Equal(telefone, result.Telefone);
            Assert.Equal(ddd, result.Ddd);
            Assert.Equal(endereco, result.Endereco);
        }


    }


    //[Fact]
    //    public void TestAddContato()
    //    {
    //        var contato = new Contato
    //        {
    //            Nome = "Teste",
    //            Email = "teste@example.com",
    //            Telefone = "123456789",
    //            Ddd = "11",
    //            Endereco = "Rua Teste"
    //        };

    //        _context.Contatos.Add(contato);
    //        _context.SaveChanges();

    //        var result = _context.Contatos.Find(contato.Id);
    //        Assert.NotNull(result);
    //        Assert.Equal("Teste", result.Nome);
    //    }
    //    [Fact]
    //    public void TestReadContato()
    //    {
    //        var contato = new Contato
    //        {
    //            Nome = "Teste",
    //            Email = "teste@example.com",
    //            Telefone = "123456789",
    //            Ddd = "11",
    //            Endereco = "Rua Teste"
    //        };

    //        _context.Contatos.Add(contato);
    //        _context.SaveChanges();

    //        var result = _context.Contatos.Find(contato.Id);
    //        Assert.NotNull(result);
    //        Assert.Equal("Teste", result.Nome);
    //    }
    //    [Fact]
    //    public void TestUpdateContato()
    //    {
    //        var contato = new Contato
    //        {
    //            Nome = "Teste",
    //            Email = "teste@example.com",
    //            Telefone = "123456789",
    //            Ddd = "11",
    //            Endereco = "Rua Teste"
    //        };

    //        _context.Contatos.Add(contato);
    //        _context.SaveChanges();

    //        contato.Nome = "Teste Atualizado";
    //        _context.Contatos.Update(contato);
    //        _context.SaveChanges();

    //        var result = _context.Contatos.Find(contato.Id);
    //        Assert.NotNull(result);
    //        Assert.Equal("Teste Atualizado", result.Nome);
    //    }
    //    [Fact]
    //    public void TestDeleteContato()
    //    {
    //        var contato = new Contato
    //        {
    //            Nome = "Teste",
    //            Email = "teste@example.com",
    //            Telefone = "123456789",
    //            Ddd = "11",
    //            Endereco = "Rua Teste"
    //        };

    //        _context.Contatos.Add(contato);
    //        _context.SaveChanges();

    //        _context.Contatos.Remove(contato);
    //        _context.SaveChanges();

    //        var result = _context.Contatos.Find(contato.Id);
    //        Assert.Null(result);
    //    }
}
