import { useEffect, useCallback } from "react";

export const useDebounced = (callback: () => void, dependencies: any[], delay: number = 300) => {
	const debouncedCallback = useCallback(() => {
		const handler = setTimeout(() => {
			callback();
		}, delay);

		return () => {
			clearTimeout(handler);
		};
	}, [callback, delay, ...dependencies]);

	useEffect(() => {
		const cleanup = debouncedCallback();
		return cleanup;
	}, [...dependencies]);
};
