import { typeTreatment } from "./treatment.d";

export const getTypeTreatment = (): { id: string; value: string }[] => [
	{ id: typeTreatment.cancer.toString(), value: "Câncer" },
	{ id: typeTreatment.pretransplant.toString(), value: "Pré-transplante" },
	{ id: typeTreatment.posttransplant.toString(), value: "Pós-transplante" },
	{ id: typeTreatment.other.toString(), value: "Outro" },
];

export const formatTypeTreatment = (_typeTreatment: typeTreatment): string => {
	switch (_typeTreatment) {
		case typeTreatment.cancer:
			return "Câncer";
		case typeTreatment.posttransplant:
			return "Pós-transplante";
		case typeTreatment.pretransplant:
			return "Pré-transplante";
		case typeTreatment.other:
			return "Outro";
	}
};
