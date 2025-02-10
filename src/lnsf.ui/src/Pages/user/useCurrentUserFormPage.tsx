import { useEffect } from "react";
import { useForm } from "react-hook-form";

import { user as _user } from "@/types";
import { useUserStore } from "@/store";
import { toValue } from "@/utils";
import { useAuthStore } from "@/store/useAuthStore";

type user = _user & { newPassword: string; confirmPassword: string };

export const useCurrentUserFormPage = () => {
  const { putUser, putPassword } = useUserStore();
  const { currentUser } = useAuthStore();
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<user>({
    values: {
      id: 0,
      name: "",
      userName: "",
      email: "",
      phoneNumber: "",
      password: "",
      newPassword: "",
      confirmPassword: "",
      roles: [],
    },
  });

  useEffect(() => {
    currentUser().then((user) => toValue(user, setValue));
  }, []);

  const handleSave = () => putUser(getValues());
  const handlePassword = () =>
    putPassword({ newPassword: getValues("newPassword"), oldPassword: getValues("password") ?? "" });

  return {
    handleSave,
    handlePassword,
    control,
    register,
    handleSubmit,
    errors,
    getValues,
    setValue,
    watch,
  };
};
