export type treatment = {
	id?: number;
	name: string;
	type: typeTreatment;
};

export enum typeTreatment {
	cancer,
	pretransplant,
	posttransplant,
	other,
}

export const getTypeTreatment = (): { id: string; value: string }[] => [
	{ id: typeTreatment.cancer.toString(), value: "Câncer" },
	{ id: typeTreatment.pretransplant.toString(), value: "Pré Transplante" },
	{ id: typeTreatment.posttransplant.toString(), value: "Pós Transplante" },
	{ id: typeTreatment.other.toString(), value: "Outro" },
];
