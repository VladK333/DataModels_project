using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x00,
		A = 0x01,
		AB = 0x02,
		ABC = 0x03,
		ABCN = 0x04,
		ABN = 0x05,
		AC = 0x06,
		ACN = 0x07,
		AN = 0x08,
		B = 0x09,
		BC = 0x0A,
		BCN = 0x0B,
		BN = 0x0C,
		C = 0x0D,
		CN = 0x0E,
		N = 0x0F,
		s1 = 0x10,
		s12 = 0x11,
		s12N = 0x12,
		s1N = 0x13,
		s2 = 0x14,
		s2N = 0x15

	}

    public enum RegulatingControlModeKind : short
    {
        Unknown = 0x00,
        ActivePower = 0x01,
        Admittance = 0x02,
        CurrentFlow = 0x03,
        Fixed = 0x04,
        PowerFactor = 0x05,
        ReactivePower = 0x06,
        Temperature = 0x07,
        TimeScheduled = 0x08,
        Voltage = 0x09
    }

    public enum WindingConnection : short
    {
        Unknown = 0x00,
        A = 0x01,
        D = 0x02,
        I = 0x03,
        Y = 0x04,
        Yn = 0x05,
        Z = 0x06,
        Zn = 0x07,
    }

    //public enum TransformerFunction : short
    //{
    //	Supply = 1,				// Supply transformer
    //	Consumer = 2,			// Transformer supplying a consumer
    //	Grounding = 3,			// Transformer used only for grounding of network neutral
    //	Voltreg = 4,			// Feeder voltage regulator
    //	Step = 5,				// Step
    //	Generator = 6,			// Step-up transformer next to a generator.
    //	Transmission = 7,		// HV/HV transformer within transmission network.
    //	Interconnection = 8		// HV/HV transformer linking transmission network with other transmission networks.
    //}

    //public enum WindingType : short
    //{
    //	None = 0,
    //	Primary = 1,
    //	Secondary = 2,
    //	Tertiary = 3
    //}			
}
