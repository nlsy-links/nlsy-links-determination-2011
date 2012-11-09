﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nls.BaseAssembly;
using Nls.BaseAssembly.LinksDataSetTableAdapters;

namespace Nls.Tests.BaseFixture {
	[TestClass]
	public sealed class RelatedValuesFixture {
		#region Fields
		private static LinksDataSet _ds = new LinksDataSet();
		#endregion
		#region Structs
		//private struct Pair {
		//   private readonly Int32 _subjectTag1;
		//   private readonly Int32 _subjectTag2;
		//   public Int32 SubjectTag1 { get { return _subjectTag1; } }
		//   public Int32 SubjectTag2 { get { return _subjectTag2; } }
		//   public Pair ( Int32 subjectTag1, Int32 subjectTag2 ) {
		//      _subjectTag1 = subjectTag1;
		//      _subjectTag2 = subjectTag2;
		//   }
		//}
		#endregion
		#region Additional test attributes
		private TestContext testContextInstance;
		public TestContext TestContext { get { return testContextInstance; } set { testContextInstance = value; } }
		[ClassInitialize()]
		public static void ClassInitialize ( TestContext testContext ) {
			tblRelatedStructureTableAdapter taStructure = new tblRelatedStructureTableAdapter();
			using ( taStructure ) {
				taStructure.Fill(_ds.tblRelatedStructure);
			}
			tblRelatedValuesTableAdapter taValues = new tblRelatedValuesTableAdapter();
			using ( taValues ) {
				taValues.Fill(_ds.tblRelatedValues);
			}
		}
		[ClassCleanup()]
		public static void ClassCleanup ( ) {
		}
		#endregion
		#region FullSib Tests
		[TestMethod]
		public void FullSibDadDied ( ) {
			CompareRImplicit(BuildFullSibDadDied());
		}
		[TestMethod]
		public void FullSibDadLeft ( ) {
			CompareRImplicit(BuildFullSibDadLeft());
		}
		public void FullSibDadDistance ( ) {
			CompareRImplicit(BuildFullSibDadDistance());
		}
		[TestMethod]
		public void FullSibWhiteBread ( ) {
			CompareRImplicit( BuildFullSibWhiteBread());
		}
		#endregion
		#region HalfSib Tests
		[TestMethod]
		public void HalfSibDadDied ( ) {
			CompareRImplicit(BuildHalfSibDadDied());
		}
		[TestMethod]
		public void HalfSibDadLeft ( ) {
			CompareRImplicit(BuildHalfSibDadLeft());
		}
		[TestMethod]
		public void HalfSibDadDistance( ) {
			CompareRImplicit(BuildHalfSibDadDistance());
		}
		//[TestMethod]
		//public void HalfSibWhiteBread ( ) {
		//   CompareRImplicit( BuildHalfSibWhiteBread());
		//}
		#endregion
		#region Helpers
		private static void CompareRImplicit ( Pair[] pairs ) {
			foreach ( Pair pair in pairs ) {
				double? actualRImplicit = RetrieveRImplicit(pair.SubjectTag1, pair.SubjectTag2);
				if ( !actualRImplicit.HasValue )
					Assert.Fail(string.Format("Subjects {0} and {1} had a null RImplicit.  It should be {2}.", pair.SubjectTag1, pair.SubjectTag2, pair.R));
				else
					Assert.AreEqual(pair.R, actualRImplicit, "The RImplicit for subject tags " + pair.SubjectTag1 + " and " + pair.SubjectTag2 + " should be equal.");
			}
		}
		private static double? RetrieveRImplicit ( Int32 subjectTag1, Int32 subjectTag2 ) {
			string select = string.Format("{0}={1} AND {2}={3}",
				subjectTag1, _ds.tblRelatedStructure.Subject1TagColumn.ColumnName,
				subjectTag2, _ds.tblRelatedStructure.Subject2TagColumn.ColumnName);
			LinksDataSet.tblRelatedStructureRow[] drsStructure = (LinksDataSet.tblRelatedStructureRow[])_ds.tblRelatedStructure.Select(select);
			Trace.Assert(drsStructure.Length == 1, "There should be exactly one row returned.");
			Int32 relatedID = drsStructure[0].ID;
			LinksDataSet.tblRelatedValuesRow drValue = _ds.tblRelatedValues.FindByID(relatedID);
			if ( drValue.IsRImplicitNull() )
				return null;
			else
				return drValue.RImplicit;
		}
		#endregion
		#region BuildFullSibs
		private static Pair[] BuildFullSibDadDied ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .5f;
			pairs.Add(new Pair(103002, 103003, expectedRImplicit));
			return pairs.ToArray();
		}
		private static Pair[] BuildFullSibDadLeft ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .5f;
			pairs.Add(new Pair(801, 802, expectedRImplicit));
			pairs.Add(new Pair(801, 803, expectedRImplicit));
			pairs.Add(new Pair(802, 803, expectedRImplicit));
			pairs.Add(new Pair(4303, 4304, expectedRImplicit));
			pairs.Add(new Pair(66802, 66803, expectedRImplicit));
			pairs.Add(new Pair(66802, 66804, expectedRImplicit));
			pairs.Add(new Pair(66803, 66804, expectedRImplicit));
			pairs.Add(new Pair(533801, 533802, expectedRImplicit));
			pairs.Add(new Pair(533801, 533803, expectedRImplicit));
			pairs.Add(new Pair(533802, 533803, expectedRImplicit));
			return pairs.ToArray();
		}
		private static Pair[] BuildFullSibDadDistance ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .5f;
			pairs.Add(new Pair(1217202, 1217203, expectedRImplicit));
			pairs.Add(new Pair(1217202, 1217204, expectedRImplicit));
			return pairs.ToArray();
		}
		private static Pair[] BuildFullSibWhiteBread ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .5f;
			pairs.Add(new Pair(17401, 17402, expectedRImplicit));
			pairs.Add(new Pair(495301, 495302, expectedRImplicit));
			pairs.Add(new Pair(495301, 495303, expectedRImplicit));
			pairs.Add(new Pair(495301, 495304, expectedRImplicit));
			pairs.Add(new Pair(495302, 495303, expectedRImplicit));
			pairs.Add(new Pair(495302, 495304, expectedRImplicit));
			pairs.Add(new Pair(495303, 495304, expectedRImplicit));
			pairs.Add(new Pair(914501, 914502, expectedRImplicit));
			pairs.Add(new Pair(914501, 914503, expectedRImplicit));
			pairs.Add(new Pair(914501, 914504, expectedRImplicit));
			pairs.Add(new Pair(914502, 914503, expectedRImplicit));
			pairs.Add(new Pair(914502, 914504, expectedRImplicit));
			pairs.Add(new Pair(914503, 914504, expectedRImplicit));
			return pairs.ToArray();
		}
		#endregion
		#region BuildHalfSibs
		private static Pair[] BuildHalfSibDadDied ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .25f;
			pairs.Add(new Pair(103001, 103002, expectedRImplicit));
			pairs.Add(new Pair(103001, 103003, expectedRImplicit));
			pairs.Add(new Pair(103001, 103004, expectedRImplicit));
			pairs.Add(new Pair(103002, 103004, expectedRImplicit));//103002, 103003 are full sibs, confirmed by DeathDate & Explicit
			pairs.Add(new Pair(103003, 103004, expectedRImplicit));
			return pairs.ToArray();
		}
		private static Pair[] BuildHalfSibDadLeft ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .25f;
			pairs.Add(new Pair(4301, 4302, expectedRImplicit));
			pairs.Add(new Pair(4301, 4303, expectedRImplicit));
			pairs.Add(new Pair(4301, 4304, expectedRImplicit));//Years don't match up
			pairs.Add(new Pair(4302, 4303, expectedRImplicit));
			pairs.Add(new Pair(4302, 4304, expectedRImplicit));
			pairs.Add(new Pair(66801, 66802, expectedRImplicit));
			pairs.Add(new Pair(66801, 66803, expectedRImplicit));
			pairs.Add(new Pair(66801, 66804, expectedRImplicit));
			//pairs.Add(new Pair(66801, 66805, expectedRImplicit));//Years don't match up
			pairs.Add(new Pair(66802, 66805, expectedRImplicit));
			pairs.Add(new Pair(66803, 66805, expectedRImplicit));
			pairs.Add(new Pair(66804, 66805, expectedRImplicit));
			pairs.Add(new Pair(1217202, 1217205, expectedRImplicit));//The explicits don't agree; I could see it either way.
			pairs.Add(new Pair(1217203, 1217205, expectedRImplicit));//The explicits don't agree; I could see it either way.
			pairs.Add(new Pair(1217204, 1217205, expectedRImplicit));//The explicits don't agree; I could see it either way.
			pairs.Add(new Pair(1254201, 1254202, expectedRImplicit));
			pairs.Add(new Pair(1254201, 1254203, expectedRImplicit));
			return pairs.ToArray();
		}
		private static Pair[] BuildHalfSibDadDistance ( ) {
			List<Pair> pairs = new List<Pair>();
			const float expectedRImplicit = .25f;
			pairs.Add(new Pair(1217201, 1217202, expectedRImplicit));
			pairs.Add(new Pair(1217201, 1217203, expectedRImplicit));
			pairs.Add(new Pair(1217201, 1217204, expectedRImplicit));
			pairs.Add(new Pair(1217201, 1217205, expectedRImplicit));
			return pairs.ToArray();
		}
		//private static Pair[] BuildHalfSibWhiteBread ( ) {
		//   //const float expectedRImplicit = .25f;
		//   List<Pair> pairs = new List<Pair>();
		//   return pairs.ToArray();
		//}
		#endregion
	}
}