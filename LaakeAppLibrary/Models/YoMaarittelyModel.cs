﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaakeAppLibrary.Models;
public class YoMaarittelyModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string YoMaarittelyId { get; set; }
   public DateTime YoAlku { get; set; }
   public DateTime YoLoppu { get; set; }
}
