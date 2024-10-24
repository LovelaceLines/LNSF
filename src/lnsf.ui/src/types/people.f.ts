import { gender, maritalStatus, raceColor } from "./people.d";

export const getGender = (): { id: string; value: string }[] => [
	{ id: gender.male.toString(), value: "Masculino" },
	{ id: gender.female.toString(), value: "Feminino" },
	{ id: gender.other.toString(), value: "Outro" },
];

export const formatGender = (_gender: gender): string => {
	switch (_gender) {
		case gender.male:
			return "Masculino";
		case gender.female:
			return "Feminino";
		case gender.other:
			return "Outro";
	}
};

export const getRaceColor = (): { id: string; value: string }[] => [
	{ id: raceColor.white.toString(), value: "Branco" },
	{ id: raceColor.black.toString(), value: "Preto" },
	{ id: raceColor.brown.toString(), value: "Pardo" },
	{ id: raceColor.yellow.toString(), value: "Amarelo" },
	{ id: raceColor.indigenous.toString(), value: "Indígena" },
	{ id: raceColor.ignored.toString(), value: "Não declarado" },
];

export const formatRaceColor = (_raceColor: raceColor): string => {
	switch (_raceColor) {
		case raceColor.black:
			return "Preto";
		case raceColor.brown:
			return "Pardo";
		case raceColor.ignored:
			return "Não declarado";
		case raceColor.indigenous:
			return "Indígena";
		case raceColor.white:
			return "Branco";
		case raceColor.yellow:
			return "Amarelo";
	}
};

export const getMaritalStatus = (): { id: string; value: string }[] => [
	{ id: maritalStatus.single.toString(), value: "Solteiro(a)" },
	{ id: maritalStatus.married.toString(), value: "Casado(a)" },
	{ id: maritalStatus.separate.toString(), value: "Separado(a)" },
	{ id: maritalStatus.divorced.toString(), value: "Divorciado(a)" },
	{ id: maritalStatus.stableUnion.toString(), value: "União Estável" },
	{ id: maritalStatus.widower.toString(), value: "Viúvo(a)" },
];

export const formatMaritalStatus = (_maritalStatus: maritalStatus): string => {
	switch (_maritalStatus) {
		case maritalStatus.divorced:
			return "Divorciado(a)";
		case maritalStatus.married:
			return "Casado(a)";
		case maritalStatus.separate:
			return "Separado(a)";
		case maritalStatus.single:
			return "Solteiro(a)";
		case maritalStatus.stableUnion:
			return "União Estável";
		case maritalStatus.widower:
			return "Viúvo(a)";
	}
};
