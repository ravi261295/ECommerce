using BusinessLayer.BusinessOperations;
using SharedLayerDTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESiteWebApi.Controllers
{
    public class CartController : ApiController
    {
        CartBusinessOperations cartBusinessOperations = new CartBusinessOperations();

        [HttpPost]
        [Authorize]
        [Route("api/GetModel")]
        public IHttpActionResult Post([FromBody]ProductModelNumber productModelNumber)
        {
            ProductDTO productDTO = new ProductDTO();

            productDTO = cartBusinessOperations.SearchProductByModelNumber(productModelNumber.ModelNumber);

            if (productDTO != null)
            {
                return Ok(productDTO);
            }
            else
            {
                return BadRequest("Model number is not available");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/PurchaseProduct")]
        public IHttpActionResult Post([FromBody]ProductDTO productDTO)
        {
            ProductDTO product = new ProductDTO();
            product = cartBusinessOperations.PurchaseProductByUser(productDTO);

            if (product != null)
            {
                return Ok(product);
            }

            else
            {
                return BadRequest("Required Quantity must be less than or equal to available quatity or must not be zero");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/UserAddress")]
        public IHttpActionResult Post([FromBody]AddressDTO addressDTO)
        {
            AddressDTO address = new AddressDTO();
            address = cartBusinessOperations.GetAddress(addressDTO);

            if (address != null)
            {
                return Ok(address);
            }
            else
            {
                return BadRequest("Address is not exists");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/UserTableList")]
        public IHttpActionResult Post([FromBody]UserProductListDTO userProductListDTO)
        {
            List<UserProductListDTO> userProductListDTOs = new List<UserProductListDTO>();

            userProductListDTOs = cartBusinessOperations.GetUserProductListBusiness(userProductListDTO.Email);

            if (userProductListDTOs.Count() != 0)
            {
                return Ok(userProductListDTOs);
            }
            else
            {
                return BadRequest("No item purchased");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/PlaceOrder")]
        public IHttpActionResult Post([FromBody]PlaceOrderDTO placeOrderDTO)
        {
            if(placeOrderDTO.Email != null)
            {
                if (cartBusinessOperations.CheckUserProductListBusiness(placeOrderDTO.Email))
                {
                    return Ok("Order(s) placed successfully!!");
                }
                else
                {
                    return Ok("Order list is empty!!");
                }
            }
            else
            {
                return BadRequest("Login Again!!");
            }
        }
    }
}
