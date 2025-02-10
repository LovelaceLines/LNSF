import { ContentCopy } from "@mui/icons-material";
import { ButtonProps } from "@mui/material";

// TODO - Fix - This is not working
export const CopyButton = (): ButtonProps => ({
  fullWidth: true,
  startIcon: <ContentCopy />,
  sx: { justifyContent: "flex-start" },
});
