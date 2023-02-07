using ChapterAPI.Controllers;
using ChapterAPI.Interfaces;
using ChapterAPI.Models;
using ChapterAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestChapter.Controler
{
    public class LoginControlerTest
    {
        [Fact]
        public void LoginControler_Retornar_Usuario_Invalido()
        {
            //arange
            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel dadoslogin = new LoginViewModel();

            dadoslogin.email = "email@email.com";
            dadoslogin.senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            //acion
            var resultado = controller.Login(dadoslogin);

            //assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);


        }

        [Fact]
        public void LoginControler_Retornar_Token() 
        {
            //arange
            Usuario usuarioretorno = new Usuario();
            usuarioretorno.Email = "email@email.com";
            usuarioretorno.Senha = "123";
            usuarioretorno.Tipo = "0";

            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioretorno);

            string isurrevalidação = "chapter.webapi";

            LoginViewModel dadoslogin = new LoginViewModel();
            dadoslogin.email = "batata";
            dadoslogin.senha = "123";
            
            var controller = new LoginController(fakeRepository.Object);

            //act
            ObjectResult resultado = (ObjectResult)controller.Login(dadoslogin);

            string token = resultado.Value.ToString().Split("")[3];

            var jwtRandler = new JwtSecurityTokenHandler();
            var tokenjwt = jwtRandler.ReadJwtToken(token);

            //assert
            Assert.Equal(isurrevalidação, tokenjwt.Issuer);


        }

    }
}
