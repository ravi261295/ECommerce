using DatabaseLayer.DatabaseOperations;
using DatabaseLayer.ModelsDB;
using SharedLayerDTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessOperations
{
    public class CartBusinessOperations
    {
        CartDBOperations cartDBOperations = new CartDBOperations();

        public ProductDTO SearchProductByModelNumber(string modelNumber)
        {
            ProductDTO productDTO = new ProductDTO();

            ProductDB productDB = cartDBOperations.GetProductByModelNumber(modelNumber);

            if (productDB != null)
            {
                productDTO.ModelNumber = productDB.ModelNumber;
                productDTO.Price = productDB.Price;
                productDTO.Description = productDB.Description;
                productDTO.AvailableQuantity = productDB.AvailableQuantity;
                productDTO.DeliveryTime = productDB.DeliveryTime;

                return productDTO;
            }
            else
            {
                return null;
            }
        }

        public ProductDTO PurchaseProductByUser(ProductDTO productDTO)
        {
            return cartDBOperations.PurchaseProduct(productDTO);
        }

        public AddressDTO GetAddress(AddressDTO addressDTO)
        {
            AddressDB addressDB= new AddressDB();

            AddressDTO address = new AddressDTO();

            addressDB = cartDBOperations.GetBillingAddress(addressDTO);
            
            address.Address = addressDB.DeliveryAddress;
            address.Email = addressDB.Email;

            return address;
        }

        public List<UserProductListDTO> GetUserProductListBusiness(string email)
        {
            List<UserProductDBModel> userProductDBModel = cartDBOperations.GetUserProductsDB(email);

            List<UserProductListDTO> userProductListDTOs = new List<UserProductListDTO>();

            foreach (var x in userProductDBModel)
            {
                UserProductListDTO userProductListDTO = new UserProductListDTO();

                userProductListDTO.Email = x.Email;
                userProductListDTO.ModelNumber = x.ModelNumber;
                userProductListDTO.RequiredQuantity = x.RequiredQuantity;
                userProductListDTO.Price = x.Price;
                userProductListDTO.Description = x.Description;
                userProductListDTO.DeliveryTime = x.DeliveryTime;

                userProductListDTOs.Add(userProductListDTO);
            }

            return userProductListDTOs;
        }

        public bool CheckUserProductListBusiness(string email)
        {
            if (cartDBOperations.CheckUserProductListDb(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
