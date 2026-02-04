# MPPM Project 2025/26 – CIM, NMS and GDA

This project demonstrates a complete implementation of working with the CIM model, CIM Profile, RDFS/XMI schema, CIM/XML data and GDA communication within an NMS system, based on the diagram assigned in the Enterprise Architect tool.

Project flow:

EA diagram → CIM Profile → RDFS → Generated DLL → Adapter → Delta → NMS → GDA client

---

## Tools and Technologies

- Enterprise Architect Lite
- CIM Tool
- CIMET
- .NET / C#
- Adapter Pattern
- NMS (Network Management System)
- GDA (Generic Data Access)
- WPF / Console client

---

## Project Structure

| Project Part | Description |
|--------------|-------------|
| CIM Profile | CIM profile created based on the EA diagram |
| RDFS | Profile export in legacy-rdfs format |
| Generated DLL | DLL generated from the CIM profile |
| Adapter | Mapping Concrete Model objects to Delta objects |
| NMS | Data loading and GDA implementation |
| CIM/XML | XML file with concrete data instances |
| Client (GDA) | Application for reading data from the service |

---

## Examples of GDA Reading

- Reading all TransformerWinding objects related to the transformer with the highest GID.
- Reading an object with the minimum value of a specific property.

For a higher grade, a WPF GUI is implemented for data visualization.

---

## Running the Project

1. Set Platform Target to x86.
2. Start NMS.
3. Load CIM/XML data.
4. Run the client application.
5. Test GDA methods.

---


## What This Project Demonstrates

- Understanding of the CIM model and CIM profile
- Working with RDFS and XMI schemas
- Adapter pattern and Delta object handling
- Understanding of NMS class structure
- Full understanding of ModelCodes
- Proper use of GDA methods through the client application
