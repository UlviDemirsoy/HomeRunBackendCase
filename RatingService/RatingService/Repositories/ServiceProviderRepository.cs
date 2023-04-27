using Microsoft.EntityFrameworkCore;
using RatingService.Data;
using RatingService.Dtos;
using RatingService.Exceptions;
using RatingService.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;

namespace RatingService.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {

        private readonly AppDbContext _context;

        public ServiceProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateRating(Rating rating)
        {
            if(rating == null)
            {
                throw new ArgumentNullException(nameof(rating));
            }
            _context.Ratings.Add(rating);
        }

        public void CreateServiceProvider(ServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            _context.ServiceProviders.Add(serviceProvider);
        }

        public IEnumerable<ServiceProvider> GetAllServiceProviders()
        {
            return _context.ServiceProviders.ToList();
        }

        public IEnumerable<ServiceProviderRatingReadDto> GetAllServiceProvidersWithAverageRating()
        {
            var q = (from sp in _context.ServiceProviders
                     join r in _context.Ratings on sp.Id equals r.ServiceProviderId
                    
                     group new { sp, r } by new { sp.Id, sp.Name, sp.Description } into grp
                     select new ServiceProviderRatingReadDto
                     {
                         Id = grp.Key.Id,
                         Name = grp.Key.Name,
                         Description = grp.Key.Description,
                         AverageRating = grp.Average(x => x.r.Point)

                     }).ToList();

            return q;
        }

        public Rating GetRatingById(int id)
        {
            return _context.Ratings
                .Where(r=>r.Id == id).FirstOrDefault();
        }

        public IEnumerable<Rating> GetRatings()
        {
            return _context.Ratings
                .ToList();
        }

        public IEnumerable<Rating> GetRatingsForServiceProvider(int id)
        {
            return _context.Ratings.Where(x=>x.ServiceProviderId== id).ToList();
        }

        public ServiceProvider GetServiceProviderById(int id)
        {
            return _context.ServiceProviders.Where(x => x.Id == id).FirstOrDefault();
        }

        public ServiceProviderRatingReadDto GetServiceProviderWithAverageRatingById(int id)
        {
            var q = (from sp in _context.ServiceProviders
                     join r in _context.Ratings on sp.Id equals r.ServiceProviderId
                     where sp.Id == id
                     group new {sp, r} by new { sp.Id, sp.Name, sp.Description } into grp
                     select new ServiceProviderRatingReadDto
                     {
                        Id = grp.Key.Id,
                        Name = grp.Key.Name,
                        Description = grp.Key.Description,
                        AverageRating = grp.Average(x=>x.r.Point)

                     }).FirstOrDefault();

            if(q == null)
            {
                throw new RatingNotFoundException(id);
                
            }

            return q;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool ServiceProviderExists(int id)
        {
            var res = _context.ServiceProviders.Any(p => p.Id == id);
            if (!res)
            {
                throw new SercivePlatformNotFoundException(id);
            }

            return res;
        }

        public bool RatingExists(int id)
        {
            var res = _context.Ratings.Any(p => p.Id == id);
            if (!res)
            {
                throw new RatingNotFoundException(id);
            }

            return res;
        }


    }
}
