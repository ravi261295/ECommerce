using DatabaseLayer.Context;
using DatabaseLayer.ModelsDB;
using SharedLayerDTO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DatabaseOperations
{
    public class CartDBOperations
    {
        public ProductDB GetProductByModelNumber(string modelNumber)
        {
            using (var context = new ECommerceDB())
            {
                return context.productDB.FirstOrDefault(x => x.ModelNumber == modelNumber);
            }
        }

        public ProductDTO PurchaseProduct(ProductDTO productDTO)
        {
            ProductDB product = new ProductDB();

            UserProductDBModel userProductDBModel = new UserProductDBModel();
            
            ProductDTO productDTOObj = new ProductDTO(); 

            using (var context = new ECommerceDB())
            {
                if (context.productDB.Any(x => x.ModelNumber == productDTO.ModelNumber))
                {
                    product = context.productDB.FirstOrDefault(x => x.ModelNumber == productDTO.ModelNumber);

                    if((product.AvailableQuantity > productDTO.RequiredQuantity) && productDTO.RequiredQuantity != 0)
                    {
                        product.AvailableQuantity -= productDTO.RequiredQuantity;
                        context.SaveChanges();

                        productDTOObj.ModelNumber = productDTO.ModelNumber;
                        productDTOObj.Price = productDTO.Price;
                        productDTOObj.Description = productDTO.Description;
                        productDTOObj.AvailableQuantity = productDTO.AvailableQuantity;
                        productDTOObj.RequiredQuantity = productDTO.RequiredQuantity;
                        productDTOObj.DeliveryTime = productDTO.DeliveryTime;

                        userProductDBModel.Email = productDTO.Email;
                        userProductDBModel.ModelNumber = productDTO.ModelNumber;
                        userProductDBModel.RequiredQuantity = productDTO.RequiredQuantity;
                        userProductDBModel.Price = productDTO.Price;
                        userProductDBModel.Description = productDTO.Description;
                        userProductDBModel.DeliveryTime = productDTO.DeliveryTime;
                        context.userProductDBModels.Add(userProductDBModel);
                        context.SaveChanges();

                        return productDTOObj;
                    }

                    return null;
                }
                return null;
            }
        }

        public AddressDB GetBillingAddress(AddressDTO addressDTO)
        {
            var addressDB = new AddressDB();

            using (var context = new ECommerceDB())
            {
                if(context.addressDB.Any(x => x.Email == addressDTO.Email))
                {
                    return context.addressDB.FirstOrDefault(x => x.Email == addressDTO.Email);
                }
                else
                {
                    return null;
                }
            }
        }

        public List<UserProductDBModel> GetUserProductsDB(string email)
        {
            using (var context = new ECommerceDB())
            {
                List<UserProductDBModel> userProductList = context.userProductDBModels.Where(x => x.Email == email).ToList();


                if (userProductList != null)
                {
                    return userProductList;
                }

                else
                {
                    return null;
                }
            }
        }

        public bool CheckUserProductListDb(string email)
        {
            using(var context = new ECommerceDB())
            {
                if(context.userProductDBModels.Any(x => x.Email == email))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
