using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Users
    {
        private readonly DL.LblancasUsersPruebaTecnicaContext _context;
        public Users(DL.LblancasUsersPruebaTecnicaContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try 
            {
                var query = _context.UsersGetDTO.FromSqlRaw("UsersGetAll").ToList();
                if(query.Count > 0)
                {
                    foreach(var item in query)
                    {
                        ML.Users users = new ML.Users();
                        users.IdUser = item.IdUser;
                        users.Nombre = item.Nombre;
                        users.ApellidoPaterno = item.ApellidoPaterno;
                        users.ApellidoMaterno = item.ApellidoMaterno;
                        users.FechaNacimiento = item.FechaNacimiento.ToString("dd-MM-yyyy");
                        users.Imagen = item.Imagen;
                        users.Rol =  new ML.Rol();
                        users.Rol.IdRol = item.IdRol;
                        users.Rol.Nombre = item.NombreRol;
                        result.Objects.Add(users);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron Users";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result Add(ML.Users user)
        {
            ML.Result result = new ML.Result();
            try
            {
                var nombre = new SqlParameter("@Nombre", user.Nombre);
                var apellidoPaterno = new SqlParameter("@ApellidoPaterno", user.ApellidoPaterno);
                var apellidoMaterno = new SqlParameter("@ApellidoMaterno", user.ApellidoMaterno);
                var fechaNacimiento = new SqlParameter("@FechaNacimiento", DateTime.Parse(user.FechaNacimiento).ToString("dd-MM-yyyy"));
                var imagen = new SqlParameter("@Imagen", user.Imagen);
                var idRol = new SqlParameter("@IdRol", user.Rol.IdRol);
                var query = _context.Database.ExecuteSqlRaw("UserAdd @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Imagen, @IdRol", nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, imagen, idRol);
                if(query > 0)
                {
                    result.Correct =true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo Agregar el User";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result Update(ML.Users user)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idUser =  new SqlParameter("@IdUser", user.IdUser);
                var nombre = new SqlParameter("@Nombre", user.Nombre);
                var apellidoPaterno = new SqlParameter("@ApellidoPaterno", user.ApellidoPaterno);
                var apellidoMaterno = new SqlParameter("@ApellidoMaterno", user.ApellidoMaterno);
                var fechaNacimiento = new SqlParameter("@FechaNacimiento", DateTime.Parse(user.FechaNacimiento).ToString("dd-MM-yyyy"));
                var imagen = new SqlParameter("@Imagen", user.Imagen);
                var idRol = new SqlParameter("@IdRol", user.Rol.IdRol);
                var query = _context.Database.ExecuteSqlRaw("UserUpdate @IdUser, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Imagen, @IdRol", idUser, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, imagen, idRol);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo Actualizar el User";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result Delete(int IdUser)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idUser = new SqlParameter("@IdUser", IdUser);
                var query = _context.Database.ExecuteSqlRaw("UserDelete @IdUser", idUser);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo Eliminar el User";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result GetById(int IdUser)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idUser = new SqlParameter("@IdUser", IdUser);
                var query = _context.UsersGetByIdDTO.FromSqlRaw("UsersGetById @IdUser", idUser).AsEnumerable().SingleOrDefault();
                if (query != null)
                {
                    ML.Users user = new ML.Users();
                    user.IdUser = query.IdUser;
                    user.Nombre = query.Nombre;
                    user.ApellidoPaterno = query.ApellidoPaterno;
                    user.Email = query.Email;
                    user.Password = query.Password;
                    user.FechaNacimiento = DateTime.ParseExact(query.FechaNacimiento, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
                    user.Imagen = query.Imagen;
                    user.Rol = new ML.Rol();
                    user.Rol.IdRol = query.IdRol;
                    user.Rol.Nombre = query.NombreRol;
                    result.Object = user;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo Agregar el User";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result Login(ML.Login login)
        {
            ML.Result result = new ML.Result();
            try
            {
                var email = new SqlParameter("@Email", login.Email);

                var query = _context.LoginDTO.FromSqlRaw("LoginEmailAndPassword @Email", email).AsEnumerable().SingleOrDefault();
                if (query != null)
                {
                    if(query.Email == login.Email && query.Password == login.Password)
                    {
                        result.Correct = true;
                        result.Object = query.IdUser;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Password incorrecta";
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Email no registrado";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result GetEmailUnique( string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                var email = new SqlParameter("@Email", Email);

                var query = _context.Users.FromSqlRaw("EmailGetUnique @Email", email).AsEnumerable().SingleOrDefault();
                if (query != null)
                {
                    if (query != null)
                    {
                        result.Correct = true;
                        result.Object = query.IdUser;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Password incorrecta";
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Email no registrado";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
