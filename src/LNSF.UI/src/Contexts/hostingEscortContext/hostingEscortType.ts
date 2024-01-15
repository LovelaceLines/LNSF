export interface iHostingEscort {
  hostingId: number,
  escortId: number,
}

export interface iHostingEscortProvider {
  children: React.ReactNode
}

export interface iHostingEscortTypes {
  getEscortsByHostingId(id: number): Promise<iHostingEscort[]>;
}