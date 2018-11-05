﻿using GreenSa.Models.Tools;
using GreenSa.Models.Tools.GPS_Maps;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace GreenSa.Models.GolfModel
{
    public class Shot
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(foreignType: typeof(Club))]
        public String ClubId { get; set; }
        [OneToOne(CascadeOperations=CascadeOperation.CascadeRead)]
        public Club Club { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public string InitPlaceId { get; set; }
        [OneToOne(foreignKey: "InitPlaceId", CascadeOperations = CascadeOperation.All)]
        public MyPosition InitPlace { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public string TargetId { get; set; }
        [OneToOne(foreignKey: "TargetId", CascadeOperations = CascadeOperation.All)]
        public MyPosition Target { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public string RealShotId { get; set; }
        [OneToOne(foreignKey:"RealShotId",CascadeOperations =CascadeOperation.All)]
        public MyPosition RealShot { get; set; }

        public DateTime Date { get; set; }


        [Ignore]
        public double Distance
        {
            get
            {
                if (InitPlace == null) return 0;
                return CustomMap.DistanceTo(InitPlace.X,InitPlace.Y,RealShot.X,RealShot.Y,"M");
            }
        }
        

        public Shot(Club currentClub, MyPosition initPlace,MyPosition target, MyPosition realShot, DateTime date )
        {
            this.Club = currentClub;
            this.InitPlace = initPlace;
            this.Target = target;
            this.RealShot = realShot;
            this.Date = date;
        }
        
        public Shot()
        {

        }
        public override string ToString()
        {
            return "From "+InitPlace+", try "+Target+" but "+RealShot +" with "+Club+" the "+Date.DayOfWeek+" "+Date.Day+" "+Date.Month;
        }
        
    }
}