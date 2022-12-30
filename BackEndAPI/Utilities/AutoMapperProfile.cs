//Extends DTOs, models, automapper 
using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Models;
using System.Globalization;

namespace BackEndAPI.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {

            #region Product
            CreateMap<Product, ProductDTO>().ReverseMap();
            #endregion

        }
    }
}
