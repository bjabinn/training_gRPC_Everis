using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreditRatingService;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Services
{
    public class CreditRatingCheckService : CreditRatingCheck.CreditRatingCheckBase
    {
        private readonly ILogger<CreditRatingCheckService> _logger;
        private static readonly Dictionary<Int32, Int32> customerTrustedCredit = new Dictionary<Int32, Int32>()
        {
            {201, 10000},
            {417, 5000},
            {306, 15000}
        };
        public CreditRatingCheckService(ILogger<CreditRatingCheckService> logger)
        {
            _logger = logger;
        }

        public override Task<CreditReply> CheckCreditRequest(CreditRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreditReply
            {
                IsAccepted = IsEligibleForCredit(request.CustomerId, request.CreditQuantity)
            });
        }

        private bool IsEligibleForCredit(Int32 customerId, Int32 credit)
        {
            bool isEligible = false;

            if (customerTrustedCredit.TryGetValue(customerId, out Int32 maxCredit))
            {
                isEligible = credit <= maxCredit;
            }

            return isEligible;
        }
    }
}
