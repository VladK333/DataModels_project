namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

        #region Populate ResourceDescription
        public static void PopulateDCLineSegmentProperties(FTN.DCLineSegment cimDCLineSegment, ResourceDescription rd)
        {
            if ((cimDCLineSegment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(cimDCLineSegment, rd);
            }
        }

        public static void PopulateConductorProperties(FTN.Conductor cimConductor, ResourceDescription rd)
        {
            if ((cimConductor != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimConductor, rd);

                if (cimConductor.LengthHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CONDUCTOR_LEN, cimConductor.Length));
                }
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);
            }
        }

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulateACLineSegmentProperties(FTN.ACLineSegment cimACLineSegment, ResourceDescription rd)
		{
			if ((cimACLineSegment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductorProperties(cimACLineSegment, rd);

				if (cimACLineSegment.B0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ACLSEGMENT_B0CH, cimACLineSegment.B0ch));
				}
				if (cimACLineSegment.BchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ACLSEGMENT_BCH, cimACLineSegment.Bch));
				}
                if (cimACLineSegment.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLSEGMENT_G0CH, cimACLineSegment.G0ch));
                }
                if (cimACLineSegment.GchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLSEGMENT_GCH, cimACLineSegment.Gch));
                }
                if (cimACLineSegment.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLSEGMENT_R, cimACLineSegment.R));
                }
                if (cimACLineSegment.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLSEGMENT_R0, cimACLineSegment.R0));
                }
                if (cimACLineSegment.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLSEGMENT_X, cimACLineSegment.X));
                }
                if (cimACLineSegment.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLSEGMENT_X0, cimACLineSegment.X0));
                }
            }
		}

		public static void PopulateSeriesCompensatorResourceProperties(FTN.SeriesCompensator cimSeriesCompensator, ResourceDescription rd)
		{
			if ((cimSeriesCompensator != null) && (rd != null))
			{
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSeriesCompensator, rd);

                if (cimSeriesCompensator.RHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SERIESCOMP_R, cimSeriesCompensator.R));
				}
                if (cimSeriesCompensator.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMP_R0, cimSeriesCompensator.R0));
                }
                if (cimSeriesCompensator.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMP_X, cimSeriesCompensator.X));
                }
                if (cimSeriesCompensator.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMP_X0, cimSeriesCompensator.X0));
                }
            }
		}

		public static void PopulateConnectivityNodeResourceProperties(FTN.ConnectivityNode cimConnectivityNode, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConnectivityNode != null) && (rd != null))
			{
				// PROVJERITI DA LI TREBA I OVDJE DA SE POZIVA POPULATE
                //PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);

                if (cimConnectivityNode.DescriptionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONNODE_DESC, cimConnectivityNode.Description));
				}
                if (cimConnectivityNode.ConnectivityNodeContainerHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimConnectivityNode.ConnectivityNodeContainer.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimConnectivityNode.GetType().ToString()).Append(" rdfID = \"").Append(cimConnectivityNode.ID);
                        report.Report.Append("\" - Failed to set reference to ConnectivityNodeContainer: rdfID \"").Append(cimConnectivityNode.ConnectivityNodeContainer.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CONNODE_CONNODECON, gid));
                }
            }
		}

        public static void PopulateTerminalResourceProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConnectivityNodeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConnectivityNode.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to ConnectivityNode: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNODE, gid));
                }
                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to ConductingEquipment: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONEQUIP, gid));
                }
            }
        }

        public static void PopulateConnectivityNodeContainerResourceProperties(FTN.ConnectivityNodeContainer cimConnectivityNodeContainer, ResourceDescription rd)
		{
			if ((cimConnectivityNodeContainer != null) && (rd != null))
			{
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimConnectivityNodeContainer, rd);
            }
		}

		//public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		//{
		//	if ((cimPowerTransformer != null) && (rd != null))
		//	{
		//		PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

		//		if (cimPowerTransformer.FunctionHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)GetDMSTransformerFunctionKind(cimPowerTransformer.Function)));
		//		}
		//		if (cimPowerTransformer.AutotransformerHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTR_AUTO, cimPowerTransformer.Autotransformer));
		//		}
		//	}
		//}

		//public static void PopulateTransformerWindingProperties(FTN.TransformerWinding cimTransformerWinding, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		//{
		//	if ((cimTransformerWinding != null) && (rd != null))
		//	{
		//		PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTransformerWinding, rd, importHelper, report);

		//		if (cimTransformerWinding.ConnectionTypeHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimTransformerWinding.ConnectionType)));
		//		}
		//		if (cimTransformerWinding.GroundedHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimTransformerWinding.Grounded));
		//		}
		//		if (cimTransformerWinding.RatedSHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimTransformerWinding.RatedS));
		//		}
		//		if (cimTransformerWinding.RatedUHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimTransformerWinding.RatedU));
		//		}
		//		if (cimTransformerWinding.WindingTypeHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE, (short)GetDMSWindingType(cimTransformerWinding.WindingType)));
		//		}
		//		if (cimTransformerWinding.PhaseToGroundVoltageHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimTransformerWinding.PhaseToGroundVoltage));
		//		}
		//		if (cimTransformerWinding.PhaseToPhaseVoltageHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimTransformerWinding.PhaseToPhaseVoltage));
		//		}
		//		if (cimTransformerWinding.PowerTransformerHasValue)
		//		{
		//			long gid = importHelper.GetMappedGID(cimTransformerWinding.PowerTransformer.ID);
		//			if (gid < 0)
		//			{
		//				report.Report.Append("WARNING: Convert ").Append(cimTransformerWinding.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerWinding.ID);
		//				report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimTransformerWinding.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
		//			}
		//			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
		//		}
		//	}
		//}

		//public static void PopulateWindingTestProperties(FTN.WindingTest cimWindingTest, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		//{
		//	if ((cimWindingTest != null) && (rd != null))
		//	{
		//		PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimWindingTest, rd);

		//		if (cimWindingTest.LeakageImpedanceHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN, cimWindingTest.LeakageImpedance));
		//		}
		//		if (cimWindingTest.LoadLossHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LOADLOSS, cimWindingTest.LoadLoss));
		//		}
		//		if (cimWindingTest.NoLoadLossHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_NOLOADLOSS, cimWindingTest.NoLoadLoss));
		//		}
		//		if (cimWindingTest.PhaseShiftHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_PHASESHIFT, cimWindingTest.PhaseShift));
		//		}
		//		if (cimWindingTest.LeakageImpedance0PercentHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, cimWindingTest.LeakageImpedance0Percent));
		//		}
		//		if (cimWindingTest.LeakageImpedanceMaxPercentHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, cimWindingTest.LeakageImpedanceMaxPercent));
		//		}
		//		if (cimWindingTest.LeakageImpedanceMinPercentHasValue)
		//		{
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, cimWindingTest.LeakageImpedanceMinPercent));
		//		}

		//		if (cimWindingTest.From_TransformerWindingHasValue)
		//		{
		//			long gid = importHelper.GetMappedGID(cimWindingTest.From_TransformerWinding.ID);
		//			if (gid < 0)
		//			{
		//				report.Report.Append("WARNING: Convert ").Append(cimWindingTest.GetType().ToString()).Append(" rdfID = \"").Append(cimWindingTest.ID);
		//				report.Report.Append("\" - Failed to set reference to TransformerWinding: rdfID \"").Append(cimWindingTest.From_TransformerWinding.ID).AppendLine(" \" is not mapped to GID!");
		//			}
		//			rd.AddProperty(new Property(ModelCode.WINDINGTEST_POWERTRWINDING, gid));
		//		}
		//	}
		//}
		#endregion Populate ResourceDescription
	}
}
