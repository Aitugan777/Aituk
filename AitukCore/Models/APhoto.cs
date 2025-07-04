﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Models
{
    public class APhoto
    {
        [Key]
        public long Id { get; set; }

        public long ParentId { get; set; }

        public EPhotoFor PhotoFor { get; set; }

        public byte[] Content { get; set; }
    }
}
