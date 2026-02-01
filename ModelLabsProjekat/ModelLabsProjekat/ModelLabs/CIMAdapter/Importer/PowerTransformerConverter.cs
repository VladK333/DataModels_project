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

        // ========================================
        // CORE HIERARCHY (Base Classes)
        // ========================================

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

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NIS, cimEquipment.NormallyInService));
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

        // ========================================
        // POWER TRANSFORMER BRANCH
        // ========================================

        public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd)
        {
            if ((cimPowerTransformer != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimPowerTransformer, rd);

                if (cimPowerTransformer.VectorGroupHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANS_VECTOR, cimPowerTransformer.VectorGroup));
                }
            }
        }

        // ========================================
        // TERMINAL
        // ========================================

        public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConnectedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTED, cimTerminal.Connected));
                }
                if (cimTerminal.PhasesHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_PHASES, (short)GetDMSPhaseCode(cimTerminal.Phases)));
                }
                if (cimTerminal.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_SEQNUMBER, cimTerminal.SequenceNumber));
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

        // ========================================
        // REGULATING CONTROL BRANCH
        // ========================================

        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulatingControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd);

                if (cimRegulatingControl.DiscreteHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATIONGCONTROL_DISC, cimRegulatingControl.Discrete));
                }
                if (cimRegulatingControl.ModeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATIONGCONTROL_MODE, (short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode)));
                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATIONGCONTROL_MP, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
                }
                if (cimRegulatingControl.TargetRangeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATIONGCONTROL_TR, cimRegulatingControl.TargetRange));
                }
                if (cimRegulatingControl.TargetValueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATIONGCONTROL_TV, cimRegulatingControl.TargetValue));
                }
                if (cimRegulatingControl.TerminalHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegulatingControl.Terminal.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulatingControl.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulatingControl.ID);
                        report.Report.Append("\" - Failed to set reference to Terminal: rdfID \"").Append(cimRegulatingControl.Terminal.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULATIONGCONTROL_TERMINAL, gid));
                }
            }
        }

        public static void PopulateTapChangerControlProperties(FTN.TapChangerControl cimTapChangerControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTapChangerControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingControlProperties(cimTapChangerControl, rd, importHelper, report);

                if (cimTapChangerControl.LimitVoltageHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGERCONTROL_LV, cimTapChangerControl.LimitVoltage));
                }
                if (cimTapChangerControl.LineDropCompensationHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGERCONTROL_LDC, cimTapChangerControl.LineDropCompensation));
                }
                if (cimTapChangerControl.LineDropRHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGERCONTROL_LDR, cimTapChangerControl.LineDropR));
                }
                if (cimTapChangerControl.LineDropXHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGERCONTROL_LDX, cimTapChangerControl.LineDropX));
                }
                if (cimTapChangerControl.ReverseLineDropRHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGERCONTROL_RLDR, cimTapChangerControl.ReverseLineDropR));
                }
                if (cimTapChangerControl.ReverseLineDropXHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGERCONTROL_RLDX, cimTapChangerControl.ReverseLineDropX));
                }
            }
        }

        // ========================================
        // TAP CHANGER
        // ========================================

        public static void PopulateTapChangerProperties(FTN.TapChanger cimTapChanger, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTapChanger != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimTapChanger, rd);

                if (cimTapChanger.HighStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_HS, cimTapChanger.HighStep));
                }
                if (cimTapChanger.InitialDelayHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_INITDEL, cimTapChanger.InitialDelay));
                }
                if (cimTapChanger.LowStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_LS, cimTapChanger.LowStep));
                }
                if (cimTapChanger.LtcFlagHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_IF, cimTapChanger.LtcFlag));
                }
                if (cimTapChanger.NeutralStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_NEUS, cimTapChanger.NeutralStep));
                }
                if (cimTapChanger.NeutralUHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_NU, cimTapChanger.NeutralU));
                }
                if (cimTapChanger.NormalStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_NORMS, cimTapChanger.NormalStep));
                }
                if (cimTapChanger.RegulationStatusHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_RS, cimTapChanger.RegulationStatus));
                }
                if (cimTapChanger.SubsequentDelayHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_SD, cimTapChanger.SubsequentDelay));
                }
                if (cimTapChanger.TapChangerControlHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTapChanger.TapChangerControl.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTapChanger.GetType().ToString()).Append(" rdfID = \"").Append(cimTapChanger.ID);
                        report.Report.Append("\" - Failed to set reference to TapChangerControl: rdfID \"").Append(cimTapChanger.TapChangerControl.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TAPCHANGER_TAPCHANGERCONTROL, gid));
                }
            }
        }

        // ========================================
        // TRANSFORMER END BRANCH
        // ========================================

        public static void PopulateTransformerEndProperties(FTN.TransformerEnd cimTransformerEnd, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTransformerEnd != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTransformerEnd, rd);

                if (cimTransformerEnd.TerminalHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTransformerEnd.Terminal.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerEnd.ID);
                        report.Report.Append("\" - Failed to set reference to Terminal: rdfID \"").Append(cimTransformerEnd.Terminal.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TRANSFORMEREND_TERMINAL, gid));
                }
            }
        }

        public static void PopulatePowerTransformerEndProperties(FTN.PowerTransformerEnd cimPowerTransformerEnd, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPowerTransformerEnd != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateTransformerEndProperties(cimPowerTransformerEnd, rd, importHelper, report);

                if (cimPowerTransformerEnd.BHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_B, cimPowerTransformerEnd.B));
                }
                if (cimPowerTransformerEnd.B0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_B0, cimPowerTransformerEnd.B0));
                }
                if (cimPowerTransformerEnd.ConnectionKindHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_CK, (short)GetDMSWindingConnection(cimPowerTransformerEnd.ConnectionKind)));
                }
                if (cimPowerTransformerEnd.GHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_G, cimPowerTransformerEnd.G));
                }
                if (cimPowerTransformerEnd.G0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_G0, cimPowerTransformerEnd.G0));
                }
                if (cimPowerTransformerEnd.PhaseAngleClockHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_PAC, cimPowerTransformerEnd.PhaseAngleClock));
                }
                if (cimPowerTransformerEnd.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_R, cimPowerTransformerEnd.R));
                }
                if (cimPowerTransformerEnd.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_R0, cimPowerTransformerEnd.R0));
                }
                if (cimPowerTransformerEnd.RatedSHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_RS, cimPowerTransformerEnd.RatedS));
                }
                if (cimPowerTransformerEnd.RatedUHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_RU, cimPowerTransformerEnd.RatedU));
                }
                if (cimPowerTransformerEnd.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_X, cimPowerTransformerEnd.X));
                }
                if (cimPowerTransformerEnd.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_X0, cimPowerTransformerEnd.X0));
                }
                if (cimPowerTransformerEnd.PowerTransformerHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimPowerTransformerEnd.PowerTransformer.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimPowerTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerTransformerEnd.ID);
                        report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimPowerTransformerEnd.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.POWERTRANSEND_PTRANS, gid));
                }
            }
        }

        #endregion Populate ResourceDescription

        #region Enumerations

        public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(FTN.RegulatingControlModeKind mode)
        {
            switch (mode)
            {
                case FTN.RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.ActivePower;
                case FTN.RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.Admittance;
                case FTN.RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.CurrentFlow;
                case FTN.RegulatingControlModeKind.@fixed:
                    return RegulatingControlModeKind.Fixed;
                case FTN.RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.PowerFactor;
                case FTN.RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.ReactivePower;
                case FTN.RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.Temperature;
                case FTN.RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.TimeScheduled;
                case FTN.RegulatingControlModeKind.voltage:
                    return RegulatingControlModeKind.Voltage;
                default:
                    return RegulatingControlModeKind.Unknown;
            }
        }

        public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        {
            switch (phases)
            {
                case FTN.PhaseCode.A:
                    return PhaseCode.A;
                case FTN.PhaseCode.AB:
                    return PhaseCode.AB;
                case FTN.PhaseCode.ABC:
                    return PhaseCode.ABC;
                case FTN.PhaseCode.ABCN:
                    return PhaseCode.ABCN;
                case FTN.PhaseCode.ABN:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.AC:
                    return PhaseCode.AC;
                case FTN.PhaseCode.ACN:
                    return PhaseCode.ACN;
                case FTN.PhaseCode.AN:
                    return PhaseCode.AN;
                case FTN.PhaseCode.B:
                    return PhaseCode.B;
                case FTN.PhaseCode.BC:
                    return PhaseCode.BC;
                case FTN.PhaseCode.BCN:
                    return PhaseCode.BCN;
                case FTN.PhaseCode.BN:
                    return PhaseCode.BN;
                case FTN.PhaseCode.C:
                    return PhaseCode.C;
                case FTN.PhaseCode.CN:
                    return PhaseCode.CN;
                case FTN.PhaseCode.N:
                    return PhaseCode.N;
                case FTN.PhaseCode.s1:
                    return PhaseCode.s1;
                case FTN.PhaseCode.s12:
                    return PhaseCode.s12;
                case FTN.PhaseCode.s12N:
                    return PhaseCode.s12N;
                case FTN.PhaseCode.s1N:
                    return PhaseCode.s1N;
                case FTN.PhaseCode.s2:
                    return PhaseCode.s2;
                case FTN.PhaseCode.s2N:
                    return PhaseCode.s2N;
                default:
                    return PhaseCode.Unknown;
            }
        }

        public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection connection)
        {
            switch (connection)
            {
                case FTN.WindingConnection.A:
                    return WindingConnection.A;
                case FTN.WindingConnection.D:
                    return WindingConnection.D;
                case FTN.WindingConnection.I:
                    return WindingConnection.I;
                case FTN.WindingConnection.Y:
                    return WindingConnection.Y;
                case FTN.WindingConnection.Yn:
                    return WindingConnection.Yn;
                case FTN.WindingConnection.Z:
                    return WindingConnection.Z;
                case FTN.WindingConnection.Zn:
                    return WindingConnection.Zn;
                default:
                    return WindingConnection.Unknown;
            }
        }

        #endregion Enumerations
    }
}