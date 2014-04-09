using System;
using System.Collections.Generic;
using BaseLib.Util;

namespace BaseLib.Parse.Uniprot{
	[Serializable]
	public class UniprotEntry{
		private Dictionary<DbReferenceType, Dictionary<string, UniprotDbReference>> dbEntries;
		private Dictionary<FeatureType, List<UniprotFeature>> features;
		private UniprotFeature currentFeature;
		public string Sequence { get; set; }
		private readonly List<string> taxonomyIds = new List<string>();
		private readonly List<string> hostTaxonomyIds = new List<string>();
		private readonly List<string> keywords = new List<string>();

		public FeatureType[] GetAllFeatureTypes(){
			return ArrayUtils.GetKeys(features);
		}

		public string[] Accessions { get; set; }
		public string[] ProteinFullNames { get; set; }
		public string[] ProteinShortNames { get; set; }
		public string[] ProteinEcNumbers { get; set; }
		public string[] ProteinNames{
			get{
				if (ProteinFullNames.Length > 0){
					return ProteinFullNames;
				}
				if (ProteinShortNames.Length > 0){
					return ProteinShortNames;
				}
				return ProteinEcNumbers.Length > 0 ? ProteinEcNumbers : new string[0];
			}
		}
		public Tuple<string, string>[] GeneNamesAndTypes { get; set; } //(name, type)
		public string[] GeneNames{
			get{
				string[] result = new string[GeneNamesAndTypes.Length];
				for (int i = 0; i < result.Length; i++){
					result[i] = GeneNamesAndTypes[i].Item1;
				}
				return result;
			}
		}
		public string GeneName{
			get{
				string[] names = new string[GeneNamesAndTypes.Length];
				string[] types = new string[GeneNamesAndTypes.Length];
				for (int i = 0; i < names.Length; i++){
					names[i] = GeneNamesAndTypes[i].Item1;
					types[i] = GeneNamesAndTypes[i].Item2;
				}
				if (names.Length == 0){
					return "";
				}
				if (names.Length == 1){
					return names[0];
				}
				int ind = ArrayUtils.IndexOf(types, "primary");
				return ind >= 0 ? names[ind] : "";
			}
		}
		public string[] GeneNameTypes{
			get{
				string[] result = new string[GeneNamesAndTypes.Length];
				for (int i = 0; i < result.Length; i++){
					result[i] = GeneNamesAndTypes[i].Item2;
				}
				return result;
			}
		}
		public string[] OrganismNames { get; set; }
		public string[] UniprotNames { get; set; }
		public bool IsTrembl { get; set; }
		private static readonly DbReferenceType[] ensemblTypes = new[]{
			DbReferenceType.ensembl, DbReferenceType.ensemblBacteria, DbReferenceType.ensemblFungi,
			DbReferenceType.ensemblMetazoa, DbReferenceType.ensemblPlants, DbReferenceType.ensemblProtists
		};
		public string[] Ensp{
			get{
				List<string> result = new List<string>();
				UniprotDbReference[] refs = GetDbEntries(ensemblTypes);
				foreach (UniprotDbReference ref1 in refs){
					string[] w = ref1.GetPropertyValues("protein sequence ID");
					result.AddRange(w);
				}
				return result.ToArray();
			}
		}
		public string[] Ensg{
			get{
				List<string> result = new List<string>();
				UniprotDbReference[] refs = GetDbEntries(ensemblTypes);
				foreach (UniprotDbReference ref1 in refs){
					string[] w = ref1.GetPropertyValues("gene ID");
					result.AddRange(w);
				}
				return result.ToArray();
			}
		}
		public string[] Enst { get { return Get(ensemblTypes); } }
		public string[] Keywords{
			get{
				string[] kw = keywords.ToArray();
				Array.Sort(kw);
				return kw;
			}
		}

		public bool TaxonomyMatch(string taxonomyId){
			//string[] ids = Get("NCBI Taxonomy");
			//foreach (string id in ids){
			//    string[] a = TaxonomyInfo.GetAncestorIds(id);
			//    foreach (string aa in a){
			//        if (aa.Equals(taxonomyId)){
			//            return true;
			//        }
			//    }
			//}
			return false;
		}

		public string[] Get(DbReferenceType key){
			return !dbEntries.ContainsKey(key) ? new string[0] : ArrayUtils.GetKeys(dbEntries[key]);
		}

		public string[] Get(DbReferenceType[] keys){
			List<string> result = new List<string>();
			foreach (DbReferenceType key in keys){
				result.AddRange(Get(key));
			}
			return result.ToArray();
		}

		public UniprotDbReference[] GetDbEntries(DbReferenceType[] keys){
			List<UniprotDbReference> result = new List<UniprotDbReference>();
			foreach (DbReferenceType key in keys){
				result.AddRange(GetDbEntries(key));
			}
			return result.ToArray();
		}

		public UniprotDbReference[] GetDbEntries(DbReferenceType key){
			return !dbEntries.ContainsKey(key) ? new UniprotDbReference[0] : ArrayUtils.GetValues(dbEntries[key]);
		}

		public string[] GetNames(DbReferenceType key){
			if (!dbEntries.ContainsKey(key)){
				return new string[0];
			}
			UniprotDbReference[] x = ArrayUtils.GetValues(dbEntries[key]);
			string[] result = new string[x.Length];
			for (int i = 0; i < x.Length; i++){
				result[i] = x[i].GetPropertyValues("entry name")[0];
			}
			return result;
		}

		public void AddDbEntryProperty(string dbReferenceType, string dbReferenceId, string protertyType, string protertyValue){
			DbReferenceType type = DbReferenceType.GetDbReferenceType(dbReferenceType);
			dbEntries[type][dbReferenceId].AddProperty(protertyType, protertyValue);
		}

		public void AddDbEntry(string dbReferenceType, string dbReferenceId){
			if (dbEntries == null){
				dbEntries = new Dictionary<DbReferenceType, Dictionary<string, UniprotDbReference>>();
			}
			DbReferenceType type = DbReferenceType.GetDbReferenceType(dbReferenceType);
			if (!dbEntries.ContainsKey(type)){
				dbEntries.Add(type, new Dictionary<string, UniprotDbReference>());
			}
			if (!dbEntries[type].ContainsKey(dbReferenceId)){
				dbEntries[type].Add(dbReferenceId, new UniprotDbReference());
			}
		}

		public void AddFeature(string featureType, string featureDescription, string featureStatus, string featureId){
			if (features == null){
				features = new Dictionary<FeatureType, List<UniprotFeature>>();
			}
			FeatureType key = FeatureType.GetFeatureType(featureType);
			if (!features.ContainsKey(key)){
				features.Add(key, new List<UniprotFeature>());
			}
			currentFeature = new UniprotFeature(featureDescription, featureStatus, featureId);
			features[key].Add(currentFeature);
		}

		public void AddFeatureLocation(string featureBegin, string featureEnd){
			currentFeature.AddFeatureLocation(featureBegin, featureEnd);
		}

		public void AddFeatureVariation(string s){
			currentFeature.AddFeatureVariation(s);
		}

		public void AddFeatureOriginal(string s){
			currentFeature.AddFeatureOriginal(s);
		}

		public int GetFeatureCount(FeatureType type){
			if (features == null){
				features = new Dictionary<FeatureType, List<UniprotFeature>>();
			}
			return features.ContainsKey(type) ? features[type].Count : 0;
		}

		public List<UniprotFeature> GetFeatures(FeatureType type){
			if (features == null){
				features = new Dictionary<FeatureType, List<UniprotFeature>>();
			}
			return features.ContainsKey(type) ? features[type] : new List<UniprotFeature>();
		}

		public void AddTaxonomyId(string id){
			taxonomyIds.Add(id);
		}

		public void AddHostTaxonomyId(string id){
			hostTaxonomyIds.Add(id);
		}

		public void AddKeyword(string kw){
			keywords.Add(kw);
		}
	}
}