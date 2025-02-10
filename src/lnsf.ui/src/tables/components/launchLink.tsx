import { Launch } from "@mui/icons-material";
import { Box, IconButton } from "@mui/material";
import { Link } from "react-router-dom";

interface MRTLaunchLinkProps {
  label: any;
  to: string;
}

export const MRTLaunchLink = ({ label, to }: MRTLaunchLinkProps) => {
  return (
    <Box display="flex" alignItems="center" gap={1}>
      {label}
      <Link to={to}>
        <IconButton size="small" sx={{ height: 18, width: "auto" }}>
          <Launch />
        </IconButton>
      </Link>
    </Box>
  );
};
