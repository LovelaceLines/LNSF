import { useCallback, useRef } from "react";

//isso pode ser usado nas buscas para n acessar loucamente  banco
export const useDebounce = (delay= 500, notDelayInFirsTime = true) => {
const debouncing = useRef<NodeJS.Timeout>();
const isFirstime = useRef(notDelayInFirsTime);

    const debounce = useCallback((func: () => void) => {
        if (isFirstime.current) {
            isFirstime.current = false;
            func()
        }else{
            if (debouncing.current){
                clearTimeout(debouncing.current);
            }
            debouncing.current =  setTimeout(() => func(), delay);
        }
        
    }, [delay]);

    return {debounce};
};