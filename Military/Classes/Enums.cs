using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Military
{

    /*public enum Rank : int
    {
        Unused = 0,
        General = 1,
        LtGen = 2,
        MajGen = 3,
        BrigGen = 4,
        Col = 5,
        LtCol = 6,
        Maj = 7,
        Capt = 8,
        FirstLt = 9,
        SecondLt = 10,
        SgtMaj = 11,
        QMSgt = 12,
        OrdSgt = 13,
        FirstSgt = 14,
        Sgt = 15,
        Cpl = 16,
    }*/

    public struct Rank
    {
        int Value;

        public Rank(int value)
        {
            Value = value;
        }
        public static implicit operator int(Rank value)
        {
            return value.Value;
        }
        public static implicit operator Rank(int value)
        {
            return new Rank(value);
        }

        // These are backwards on purpose -- because the rank table is flipped upside down -- like stratego!
        public Rank PlusOne { get { return this - 1; } }
        public Rank MinusOne { get { return this + 1; } }

        public const int InvalidRank = -1;
        public static readonly Rank Invalid = InvalidRank;
        public static readonly Rank Unused = 0;
        public static readonly Rank General = 1;
        public static readonly Rank LtGen = 2;
        public static readonly Rank MajGen = 3;
        public static readonly Rank BrigGen = 4;
        public static readonly Rank Col = 5;
        public static readonly Rank LtCol = 6;
        public static readonly Rank Maj = 7;
        public static readonly Rank Capt = 8;
        public static readonly Rank FirstLt = 9;
        public static readonly Rank SecondLt = 10;
        public static readonly Rank SgtMaj = 11;
        public static readonly Rank QMSgt = 12;
        public static readonly Rank OrdSgt = 13;
        public static readonly Rank FirstSgt = 14;
        public static readonly Rank Sgt = 15;
        public static readonly Rank Cpl = 16;

        public int AsInt { get { return Value; } }
    }

    public struct PersonStatus
    {
        int Status;

        public PersonStatus(int status)
        {
            Status = status;
        }
        public static implicit operator int(PersonStatus status)
        {
            return status.Status;
        }
        public static implicit operator PersonStatus(int status)
        {
            return new PersonStatus(status);
        }

        public static readonly PersonStatus Invalid = -1;
        public static readonly PersonStatus Healthy = 0;
        public static readonly PersonStatus Killed = 1;
        public static readonly PersonStatus Wounded = 2;
        public static readonly PersonStatus Missing = 3;
        public static readonly PersonStatus Captured = 4;

        public string Identifier
        {
            get
            {
                switch (Status)
                {
                    case -1: return "i";
                    case 0: return "h";
                    case 1: return "k";
                    case 2: return "w";
                    case 3: return "m";
                    case 4: return "c";
                }
                return "";
            }
        }
        
        public bool IsKilled { get { return this == Killed; } }
        public bool IsWounded { get { return this == Wounded; } }
        public bool IsMissing { get { return this == Missing; } }
        public bool IsHealthy { get { return this == Healthy; } }
        public bool IsCaptured { get { return this == Captured; } }
    }
}
