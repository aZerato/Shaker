﻿using System;
using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Posts
{
    public class Post : IBaseEntity
    {
        public Post()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime Creation { get; set; }
    }
}
