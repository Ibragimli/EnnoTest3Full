using EnnoTest3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnnoTest3.Services
{
    public class LayoutService
    {
        private readonly DataContext _context;

        public LayoutService(DataContext context)
        {
            _context = context;
        }

        public  async Task<List<Setting>> GetSettings()
        {
            return await _context.Settings.ToListAsync();
        }
    }
}
