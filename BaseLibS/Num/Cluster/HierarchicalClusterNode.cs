using System;
using System.IO;

namespace BaseLibS.Num.Cluster{
	[Serializable]
	public class HierarchicalClusterNode{
		/// <summary>
		/// The distance between the two children.
		/// </summary>
		public double distance;
		/// <summary>
		/// The id of the left child. If it is >=0, it means the left child is an original 
		/// data point and the value is its index. When negative the left child is the <c>-left-1</c>
		/// entry in the cluster node list.
		/// </summary>
		public int left;
		/// <summary>
		/// The id of the right child. If it is >=0, it means the right child is an original 
		/// data point and the value is its index. When negative the right child is the <c>-right-1</c>
		/// entry in the cluster node list.
		/// </summary>
		public int right;

		public HierarchicalClusterNode(BinaryReader reader){
			distance = reader.ReadDouble();
			left = reader.ReadInt32();
			right = reader.ReadInt32();
		}

		public HierarchicalClusterNode() {}

		public void Write(BinaryWriter writer){
			writer.Write(distance);
			writer.Write(left);
			writer.Write(right);
		}

		public void Flip(){
			int tmp = left;
			left = right;
			right = tmp;
		}
	}
}