using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Models;
using BackEndAPI.Services.Contract;
using System.Runtime.InteropServices;
using BackEndAPI.Utilities;


namespace BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductServices productServices,
            IMapper mapper)
        {
            _productService= productServices;
            _mapper= mapper;

        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            ResponseApi<List<ProductDTO>> _response = new ResponseApi<List<ProductDTO>>();

            try
            {
                List<Product> productList = await _productService.GetList();

                if (productList.Count > 0)
                {
                    List<ProductDTO> dtoList = _mapper.Map<List<ProductDTO>>(productList);
                    _response = new ResponseApi<List<ProductDTO>>
                    {
                        Status = true,
                        Msg = "Ok",
                        Value = dtoList
                    };

                }
                else
                {
                    _response = new ResponseApi<List<ProductDTO>>
                    {
                        Status = false,
                        Msg = "No data"
                    };
                }

                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseApi<List<ProductDTO>>
                {
                    Status = false,
                    Msg = ex.Message,
                };

                return StatusCode(StatusCodes.Status500InternalServerError, _response);

            }

        }

        [HttpPost]

        public async Task<IActionResult> Post(ProductDTO request)
        {
            ResponseApi<ProductDTO> _response = new ResponseApi<ProductDTO>();

            try
            {
                Product _model = _mapper.Map<Product>(request);
                Product _productCreated = await _productService.Add(_model);

                if (_productCreated.IdItem != 0)
                {
                    _response = new ResponseApi<ProductDTO>
                    {
                        Status = true,
                        Msg = "Product Created",
                        Value = _mapper.Map<ProductDTO>(_productCreated)
                    };
                }
                else
                {
                    _response = new ResponseApi<ProductDTO>
                    {
                        Status = false,
                        Msg = "Could not create"
                    };

                }

                return StatusCode(StatusCodes.Status200OK, _response);


            }
            catch (Exception ex)
            {
                _response = new ResponseApi<ProductDTO>
                {
                    Status = false,
                    Msg = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, _response);

            }

        }

      
        [HttpPut]

        public async Task<IActionResult> Put(ProductDTO request)
        {
            ResponseApi<ProductDTO> _response = new ResponseApi<ProductDTO>();

            try
            {
                Product _model = _mapper.Map<Product>(request);
                Product _productEdited = await _productService.Update(_model);

                _response = new ResponseApi<ProductDTO>
                {
                    Status = true,
                    Msg = "Edited succesfully",
                    Value = _mapper.Map<ProductDTO>(_productEdited)
                };

                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseApi<ProductDTO>
                {
                    Status = false,
                    Msg = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, _response);

            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            ResponseApi<bool> _response = new ResponseApi<bool>();

            try
            {
                Product _productFound = await _productService.Get(id);

                bool deleted = await _productService.Delete(_productFound);

                if (deleted)
                    _response = new ResponseApi<bool>
                    {
                        Status = true,
                        Msg = "Delete succesfully"
                    };
                else
                    _response = new ResponseApi<bool>
                    {
                        Status = false,
                        Msg = "Not deleted"
                    };

                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseApi<bool>
                {
                    Status = false,
                    Msg = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

    }
}
