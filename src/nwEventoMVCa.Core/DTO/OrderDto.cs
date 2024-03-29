﻿using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
