using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IimageManagementSerives _imageManagementSerives;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }


        public UnitOfWork(AppDbContext context , IMapper mapper, IimageManagementSerives imageManagementSerives)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementSerives = imageManagementSerives;



            CategoryRepository = new CategoryRepositry(context);
            PhotoRepository = new PhotoRepository(context);
            ProductRepository = new ProductRepository(context , mapper , imageManagementSerives);


        }


    }
}
