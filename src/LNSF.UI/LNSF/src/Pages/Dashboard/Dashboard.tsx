import { BasePageLayout } from "../../Component/Layouts"
import { LateralMenu } from "../../Component"

interface IDashboardProps {
    children?: React.ReactNode;
}

export const Dashboard: React.FC<IDashboardProps> = ({ children }) => {
    return (
        <LateralMenu>
            <BasePageLayout>
                    {children}
            </BasePageLayout>
                  
        </LateralMenu>
    )
}