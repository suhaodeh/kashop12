using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using KASHOP12.DAL.Repository;
using Mapster;

namespace KASHOP12.BLL.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository, IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<BaseResponse> AddReviewAsync(string userId,int productId, CreateReviewRequest request)
        {
            var hasDelivered = await _orderRepository.HasUserDeliveredOrderForProduct(userId,productId);
            if (!hasDelivered)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "you can only review product you have recived"
                };

            }

            var alreadyReview = await _reviewRepository.HasUserReviewdProduct(userId, productId);
            if (alreadyReview)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "cant review"
                };

            }

            var review = request.Adapt<Review>();
            review.UserId = userId;
            review.ProductId = productId;
            await _reviewRepository.CreateAsync(review);
            return new BaseResponse
            {
                Success = true,
                Message = " review added successfully"
            };


        }
    }
}
