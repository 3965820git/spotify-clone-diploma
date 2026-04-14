import { apiClient } from '@/shared/api/client'

export type UploadImageResponse = {
  imageId: string
}

export type ImageDetailsResponse = {
  imageId: string
  webpUrl: string
}

export async function uploadImage(file: File) {
  const formData = new FormData()
  formData.append('File', file)

  const { data } = await apiClient.post<UploadImageResponse>(
    '/media/images',
    formData,
    {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    },
  )

  return data
}

export async function getImageDetails(imageId: string) {
  const { data } = await apiClient.get<ImageDetailsResponse>(
    '/media/images',
    {
      params: { ImageId: imageId },
    },
  )

  return data
}