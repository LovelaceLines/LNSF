import { createContext, useContext } from "react";

import { useLocalStorage } from "@/hooks";

// Renomar para os layers que serão utilizados
type LayersId = "default" | "layer2";

type Layers = {
  [key in LayersId]: boolean;
};

interface ILayersContextProps {
  layers: Layers;
  toggleLayer: (id: LayersId) => void;
}

export const LayersContext = createContext({} as ILayersContextProps);

export const LayersProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const [layers, setLayers] = useLocalStorage("layers", { default: true } as Layers);

  const toggleLayer = (id: LayersId) =>
    setLayers((layers: Layers) => {
      return { ...layers, [id]: !layers[id] };
    });

  return <LayersContext.Provider value={{ layers, toggleLayer }}>{children}</LayersContext.Provider>;
};

export const useLayer = (id: LayersId): { visible: boolean; toggleLayer: () => void } => {
  const { layers, toggleLayer } = useContext(LayersContext);
  return { visible: layers[id], toggleLayer: () => toggleLayer(id) };
};
