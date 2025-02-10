import { gender, maritalStatus, raceColor } from "./people.d";
import { typeTreatment } from "./treatment.d";

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

export const formatTypeTreatment = (_typeTreatment: typeTreatment): string => {
  switch (_typeTreatment) {
    case typeTreatment.cancer:
      return "Câncer";
    case typeTreatment.posttransplant:
      return "Pós Transplante";
    case typeTreatment.pretransplant:
      return "Pré Transplante";
    case typeTreatment.other:
      return "Outro";
  }
};
