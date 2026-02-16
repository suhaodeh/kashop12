using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Models;

namespace KASHOP12.DAL.Repository
{
  public  interface IReviewRepository
    {
        Task<bool> HasUserReviewdProduct(string userId, int productId);
        Task<Review> CreateAsync(Review Request);
    }
}
