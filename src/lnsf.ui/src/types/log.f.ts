export const getEntityName = (): { id: string; value: string }[] => [
  { id: "EmergencyContact", value: "Contato de Emergência" },
  { id: "Escort", value: "Acompanhante" },
  { id: "FamilyGroupProfile", value: "Grupo Familiar" },
  { id: "Hospital", value: "Hospital" },
  { id: "Hosting", value: "Reserva" },
  { id: "HostingEscort", value: "Acompanhante da Reserva" },
  { id: "Patient", value: "Paciente" },
  { id: "PatientTreatment", value: "Tratamento do Paciente" },
  { id: "People", value: "Pessoa" },
  { id: "PeopleRoomHosting", value: "Hospedagem" },
  { id: "Role", value: "Função" },
  { id: "Room", value: "Apartamento" },
  { id: "Tour", value: "Registro Diário" },
  { id: "Treatment", value: "Tratamento" },
  { id: "User", value: "Usuário" },
  { id: "UserAndPassword", value: "Usuário/Senha" },
  { id: "UserRole", value: "Função do Usuário" },
];

export const formatEntityName = (entityName: string): string => {
  switch (entityName) {
    case "EmergencyContact":
      return "Contato de Emergência";
    case "Escort":
      return "Acompanhante";
    case "FamilyGroupProfile":
      return "Grupo Familiar";
    case "Hospital":
      return "Hospital";
    case "Hosting":
      return "Reserva";
    case "HostingEscort":
      return "Acompanhante da Reserva";
    case "Patient":
      return "Paciente";
    case "PatientTreatment":
      return "Tratamento do Paciente";
    case "People":
      return "Pessoa";
    case "PeopleRoomHosting":
      return "Hospedagem";
    case "Role":
      return "Função";
    case "Room":
      return "Apartamento";
    case "Tour":
      return "Registro Diário";
    case "Treatment":
      return "Tratamento";
    case "User":
      return "Usuário";
    case "UserAndPassword":
      return "Usuário";
    case "UserRole":
      return "Função do Usuário";
    default:
      return entityName;
  }
};

export const getAction = (): { id: string; value: string }[] => [
  { id: "Added", value: "Adicionado" },
  { id: "Modified", value: "Modificado" },
  { id: "Deleted", value: "Deletado" },
];

export const formatAction = (action: string): string => {
  switch (action) {
    case "Added":
      return "Adicionado";
    case "Modified":
      return "Modificado";
    case "Deleted":
      return "Deletado";
    default:
      return action;
  }
};
