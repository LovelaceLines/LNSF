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
